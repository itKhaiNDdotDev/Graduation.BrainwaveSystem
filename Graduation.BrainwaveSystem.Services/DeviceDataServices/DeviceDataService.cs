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
        if (_context.DeviceDatas == null)
            throw new Exception("Entity DeviceDatas is not exist. Please check and try again.");
        var dataRecords = await _context.DeviceDatas.Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.CreatedTime)
            .Take(n).ToListAsync();

        if (_context.Data8BandsEEGs == null)
            throw new Exception("Entity Data8BandsEEGs is not exist. Please check and try again.");
        var data8Bands = new List<Data8BandsEEG>();
        foreach(var record in dataRecords)
        {
            var item = await _context.Data8BandsEEGs.Where(d => d.DeviceDataId == record.Id)
                .FirstOrDefaultAsync();
            data8Bands.Add(item ?? new Data8BandsEEG());
        }

        dataRecords = dataRecords.OrderBy(d => d.CreatedTime).ToList();
        data8Bands = data8Bands.OrderBy(d => d.CreatedTime).ToList();
        return (dataRecords, data8Bands);
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
            DeviceDataId = dataId
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
                    DeviceDataId = dataId
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
}
