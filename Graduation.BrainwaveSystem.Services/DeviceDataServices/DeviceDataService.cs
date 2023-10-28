using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Graduation.BrainwaveSystem.Services.DeviceDataServices;
using Graduation.BrainwaveSystem.Services.Data8BandsEEGServices;
using Graduation.BrainwaveSystem.Services.DataRawEEGServices;
using Graduation.BrainwaveSystem.Cores.MLDotNETModels;
using Graduation.BrainwaveSystem.Models.DTOs.AIModels;

namespace Graduation.BrainwaveSystem.Services.DeviceDataService;

public class DeviceDataService : BaseService<DeviceData, DeviceDataRequest>, IDeviceDataService
{
    private readonly DataContext _context;

    public DeviceDataService(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(List<DeviceData> GeneralExtractions, List<Data8BandsEEG> Data8Bands)> GetLastNRecords(Guid deviceId, int n = 1)
    {
        //if (_context.DeviceDatas == null)
        //    throw new Exception("Entity DeviceDatas is not exist. Please check and try again.");
        //var dataRecords = await _context.DeviceDatas.Where(x => x.DeviceId == deviceId)
        //    .OrderByDescending(x => x.CreatedTime)
        //    .Take(n).ToListAsync();

        //if (_context.Data8BandsEEGs == null)
        //    throw new Exception("Entity Data8BandsEEGs is not exist. Please check and try again.");
        //var data8Bands = new List<Data8BandsEEG>();
        //foreach(var record in dataRecords)
        //{
        //    var item = await _context.Data8BandsEEGs.Where(d => d.DeviceDataId == record.Id)
        //        .FirstOrDefaultAsync();
        //    data8Bands.Add(item ?? new Data8BandsEEG());
        //}

        if (_context.DeviceDatas == null || _context.Data8BandsEEGs == null)
            throw new Exception("Entity DeviceDatas or Data8BandsEEGs does not exist. Please check and try again.");
        var dataRecords = await (from deviceData in _context.DeviceDatas
                                 join data8BandsEEG in _context.Data8BandsEEGs
                                 on deviceData.Id equals data8BandsEEG.DeviceDataId into Details
                                 from data8BandEEG in Details.DefaultIfEmpty()
                                 where deviceData.DeviceId == deviceId
                                 orderby deviceData.CreatedTime descending
                                 select new { deviceData, data8BandEEG }).Take(n).ToListAsync();

        var deviceDatas = dataRecords.Select(data => data.deviceData).OrderBy(d => d.CreatedTime).ToList();
        var data8Bands = dataRecords.Select(data => data.data8BandEEG).OrderBy(d => d.CreatedTime).ToList();
        return (deviceDatas, data8Bands);
    }

    public async Task<Guid> Create(Guid deviceId, DataDeviceSendRequest request)
    {
        //throw new NotImplementedException();
        //Thêm DeviceData
        if(request.General == null)
            request.General = new DeviceDataRequest(); // Nếu không có các extraction thì vẫn add 1 bản ghi ma để ánh xạ Raw
        request.General.DeviceId = deviceId;
        var dataId = await base.Create(request.General);
        if (deviceId == Guid.Empty)
            throw new Exception("Insert failed! Please contact KhaiND to check problem.");

        // Thêm Data8BandsEEG với DeviceDataId vừa thêm
        var eeg8BandsRequest = new Data8BandsEEGRequest() // thay bằng Mapper chứ
        {
            // Sẽ có thể có khả nhiều bản ghi rác (null value in all fields)
            Delta = request.Delta,
            Theta = request.Theta,
            Alpha = request.Alpha,
            LowBeta = request.LowBeta,
            MidBeta = request.MidBeta,
            HighBeta = request.HighBeta,
            Gamma = request.Gamma,
            UHFGamma = request.UHFGamma,
            DeviceDataId = dataId,
            CreatedBy = request.CreatedBy,
            LastModifiedBy = request.LastModifiedBy,
        };
        //if (eeg8BandsRequest != null)
        //{
        Data8BandsEEGService eeg8BandsService = new Data8BandsEEGService(_context);
        //eeg8BandsRequest.DeviceDataId = dataId;
        var eeg8BandsId = await eeg8BandsService.Create(eeg8BandsRequest);
        if (eeg8BandsId == Guid.Empty)
            throw new Exception("Insert some data records failed! Please contact KhaiND to check details.");
        //}

        // Thêm DataRawEEG với DeviceDataId vừa thêm
        if (request.RawEEGs != null && request.RawEEGs.Any())
        {
            DataRawEEGService eegRawService = new DataRawEEGService(_context);
            foreach (int RawValue in request.RawEEGs)
            {
                var eegRaw = new DataRawEEGRequest
                {
                    Value = RawValue,
                    DeviceDataId = dataId,
                    LastModifiedBy = request.LastModifiedBy,
                    CreatedBy = request.CreatedBy,
                };
                // Nên cân nhắc thay thế thành AddRange và 1 lần SaveChange duy nhất (viết method add List Raw trong RawService)
                var eegRawId = await eegRawService.Create(eegRaw);
                if (eegRawId == Guid.Empty)
                    throw new Exception("Insert some data records failed! Please contact KhaiND to check details.");
            }
        } 

        return dataId;
    }

    public override async Task<int> Delete(Guid id)
    {
        int countAffacted = 0;

        // Xóa các bản ghi DataRawEEG tương ứng DeviceDataId
        if(_context.DataRawEEGs != null)
        {
            var eegRawRecords = _context.DataRawEEGs.Where(x => (x.DeviceDataId == id && x.IsDeleted == false));
            if(eegRawRecords.Any())
            {
                foreach (var eegRawRecord in eegRawRecords)
                {
                    eegRawRecord.IsDeleted = true;
                    eegRawRecord.LastModifiedBy = "KhaiND";
                    eegRawRecord.LastModifiedTime = DateTime.Now;
                }
                _context.DataRawEEGs.UpdateRange(eegRawRecords);
            }
        }

        // Xóa các bản ghi Data8BandsEEG tương ứng DeviceDataId
        if(_context.Data8BandsEEGs != null)
        {
            var eeg8BandsRecord = _context.Data8BandsEEGs.Where(x => (x.DeviceDataId == id && x.IsDeleted == false)).FirstOrDefault();
            if(eeg8BandsRecord != null)
            {
                eeg8BandsRecord.IsDeleted = true;
                eeg8BandsRecord.LastModifiedBy = "KhaiND";
                eeg8BandsRecord.LastModifiedTime = DateTime.Now;
                _context.Data8BandsEEGs.Update(eeg8BandsRecord);
            }
        }

        countAffacted += await _context.SaveChangesAsync();
        // Xóa bản ghi DevieData
        countAffacted += await base.Delete(id);
        return countAffacted;
    }

    public override async Task<int> DeleteForever(Guid id)
    {
        int countAffacted = 0;

        // Xóa các bản ghi DataRawEEG tương ứng DeviceDataId
        if (_context.DataRawEEGs != null)
        {
            _context.DataRawEEGs.RemoveRange(_context.DataRawEEGs.Where(d => d.DeviceDataId == id));
        }

        // Xóa các bản ghi Data8BandsEEG tương ứng DeviceDataId
        if (_context.Data8BandsEEGs != null)
        {
            var eeg8BandRecord = _context.Data8BandsEEGs.Where(d => d.DeviceDataId == id).FirstOrDefault();
            if(eeg8BandRecord != null)
                _context.Data8BandsEEGs.Remove(eeg8BandRecord);
        }

        countAffacted += await _context.SaveChangesAsync();
        // Xóa bản ghi DevieData
        countAffacted += await base.Delete(id);
        return countAffacted;
    }

    //public async Task<Guid> Create(DeviceDataRequest request)
    //{
    //    if (_context.DeviceDatas == null)
    //    {
    //        throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
    //    }

    //    var deviceData = new Models.Entities.DeviceData()
    //    {
    //        Id = new Guid(),
    //        PoorQuality = request.PoorQuality,
    //        Attention = request.Attention,
    //        Meditation = request.Meditation,
    //        DeviceId = _context.Devices.First().Id, // Cần link đến Device và thay thế bằng ID Device tương ứng
    //        IsDeleted = false,
    //        CreatedTime = DateTime.Now,
    //        CreatedBy = "KhaiND", // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
    //        LastModifiedTime = DateTime.Now,
    //        LastModifiedBy = "KhaiND" // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
    //    };
    //    _context.DeviceDatas.Add(deviceData);

    //    await _context.SaveChangesAsync();
    //    return deviceData.Id;
    //}

    //public async Task<int> Update(Guid id, DeviceDataRequest request)
    //{
    //    if (_context.DeviceDatas == null) // Hơi lặp với GetById
    //    {
    //        throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
    //    }

    //    //_context.Entry(device).State = EntityState.Modified;
    //    var deviceData = await GetById(id);
    //    deviceData.PoorQuality = request.PoorQuality;
    //    deviceData.Attention = request.Attention;
    //    deviceData.Meditation = request.Meditation;
    //    deviceData.LastModifiedTime = DateTime.Now;
    //    deviceData.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

    //    // Có thể dùng Entry.Modified như trên
    //    _context.DeviceDatas.Update(deviceData);
    //    try
    //    {
    //        return await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!DeviceDataExists(id))
    //        {
    //            throw new Exception($"404 - Not Found: DeviceData record with id [{id}] is not exist.");
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }
    //}

    //public async Task<int> Delete(Guid id)
    //{
    //    if (_context.DeviceDatas == null) // Hơi lặp với GetById
    //    {
    //        throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
    //    }

    //    var deviceData = await GetById(id);
    //    deviceData.IsDeleted = true;
    //    deviceData.LastModifiedTime = DateTime.Now;
    //    deviceData.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

    //    // Có thể dùng Entry.Modified như trên ví dụ Update
    //    _context.DeviceDatas.Update(deviceData);
    //    try
    //    {
    //        return await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!DeviceDataExists(id))
    //        {
    //            throw new Exception($"404 - Not Found: DeviceData record with id [{id}] is not exist.");
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }
    //}

    //private bool DeviceDataExists(Guid id)
    //{
    //    return (_context.DeviceDatas?.Any(e => e.Id == id)).GetValueOrDefault();
    //}

    public TgamExtractionPredictResponse GetTrainSSAPredictOutput(Guid deviceId)
    {
        var inputData = GetLastNRecords(deviceId, 120).Result;

        var inputPoorQualities = inputData.GeneralExtractions.Select(x => x.PoorQuality ?? 0).ToList();
        var resultPoorQualities = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputPoorQualities, 30, false);
        var inputAttendtions = inputData.GeneralExtractions.Select(x => x.Attention ?? 0).ToList();
        var resultAttendtions = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputAttendtions, 30, false);
        var inputMediations = inputData.GeneralExtractions.Select(x => x.Meditation ?? 0).ToList();
        var resultMediations = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputMediations, 30, false);

        var inputDeltas = inputData.Data8Bands.Select(x => x.Delta ?? 0).ToList();
        var resultDeltas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputDeltas, 30, false);;
        var inputThetas = inputData.Data8Bands.Select(x => x.Theta ?? 0).ToList();
        var resultThetas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputThetas, 30, false);
        var inputAlphas = inputData.Data8Bands.Select(x => x.Alpha ?? 0).ToList();
        var resultAlphas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputAlphas, 30, false);
        var inputLowBetas = inputData.Data8Bands.Select(x => x.LowBeta ?? 0).ToList();
        var resultLowBetas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputLowBetas, 30, false);
        var inputMidBetas = inputData.Data8Bands.Select(x => x.MidBeta ?? 0).ToList();
        var resultMidBetas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputMidBetas, 30, false);
        var inputHighBetas = inputData.Data8Bands.Select(x => x.HighBeta ?? 0).ToList();
        var resultHighBetas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputHighBetas, 30, false);
        var inputGammas = inputData.Data8Bands.Select(x => x.Gamma ?? 0).ToList();
        var resultGammas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputGammas, 30, false);
        var inputUHFGammas = inputData.Data8Bands.Select(x => x.UHFGamma ?? 0).ToList();
        var resultUHFGammas = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputUHFGammas, 30, false);

        var timeList = new List<DateTime>(30);
        var startTime = inputData.GeneralExtractions.Max(x => x.CreatedTime).AddSeconds(1);
        for (int i = 0; i < 30; i++)
        {
            timeList.Add(startTime.AddSeconds(i));
        }

        return new TgamExtractionPredictResponse()
        {
            PoorQuality = resultPoorQualities.Prediction.ForecastedValues,
            Attention = resultAttendtions.Prediction.ForecastedValues,
            Meditation = resultMediations.Prediction.ForecastedValues,
            Delta = resultDeltas.Prediction.ForecastedValues,
            Theta = resultThetas.Prediction.ForecastedValues,
            Alpha = resultAlphas.Prediction.ForecastedValues,
            LowBeta = resultLowBetas.Prediction.ForecastedValues,
            MidBeta = resultMidBetas.Prediction.ForecastedValues,
            HighBeta = resultHighBetas.Prediction.ForecastedValues,
            Gamma = resultGammas.Prediction.ForecastedValues,
            UHFGamma = resultUHFGammas.Prediction.ForecastedValues,
            PredictTimes = timeList,
            MAEGeneral = (resultPoorQualities.Evaluation.MAE + resultAttendtions.Evaluation.MAE
                    + resultMediations.Evaluation.MAE) / 3,
            RMSEGeneral = (resultPoorQualities.Evaluation.RMSE + resultAttendtions.Evaluation.RMSE
                    + resultMediations.Evaluation.RMSE) / 3,
            MAEFor8Band = (resultDeltas.Evaluation.MAE + resultThetas.Evaluation.MAE
                    + resultAlphas.Evaluation.MAE + resultLowBetas.Evaluation.MAE
                    + resultMidBetas.Evaluation.MAE + resultHighBetas.Evaluation.MAE
                    + resultGammas.Evaluation.MAE + resultUHFGammas.Evaluation.MAE) / 8,
            RMSEFor8Band = (resultDeltas.Evaluation.RMSE + resultThetas.Evaluation.RMSE
                    + resultAlphas.Evaluation.RMSE + resultLowBetas.Evaluation.RMSE
                    + resultMidBetas.Evaluation.RMSE + resultHighBetas.Evaluation.RMSE
                    + resultGammas.Evaluation.RMSE + resultUHFGammas.Evaluation.RMSE) / 8
        };
    }
}
