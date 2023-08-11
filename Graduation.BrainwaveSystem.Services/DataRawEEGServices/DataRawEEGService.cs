using Graduation.BrainwaveSystem.Cores.DataProcessors;
using Graduation.BrainwaveSystem.Cores.MLDotNETModels;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.DTOs.AIModels;
using Graduation.BrainwaveSystem.Models.DTOs.ViewModels;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graduation.BrainwaveSystem.Cores.MLDotNETModels.RegressionPredictor;

namespace Graduation.BrainwaveSystem.Services.DataRawEEGServices
{
    public class DataRawEEGService : BaseService<DataRawEEG, DataRawEEGRequest>, IDataRawEEGService
    {
        private readonly DataContext _context;

        public DataRawEEGService(DataContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<List<DataRawEEGResponse>> GetLastNDataRecords(Guid deviceId, int n = 1)
        public async Task<DataRawEEGResponse> GetLastNDataRecords(Guid deviceId, int n = 1)
        {
            ////Lấy Id n bản ghi gần nhất tương ứng thiết bị
            //if (_context.DeviceDatas == null)
            //    throw new Exception("Entity DeviceDatas is not exist. Please check and try again.");
            //var dataRecords = /*(List<DataMapRecord>)*/ await _context.DeviceDatas.Where(x => x.DeviceId == deviceId)
            //    //.Select(x => new DataMapRecord { Id = x.Id, CreatedTime = x.CreatedTime })
            //    .OrderByDescending(x => x.CreatedTime)
            //    .Select(x => x.Id)
            //    .Take(n).ToListAsync();

            //if (_context.DataRawEEGs == null)
            //    throw new Exception("Entity DataRawEEGs is not exist. Please check and try again.");
            //var result = await _context.DataRawEEGs.Where(r => dataRecords.Contains(r.DeviceDataId))
            //    .Select(r => new DataRawEEGResponse { Value = r.Value, RecivedTime = r.CreatedTime })
            //    .OrderBy(d => d.RecivedTime).ToArrayAsync();

            if (_context.DeviceDatas == null || _context.DataRawEEGs == null)
                throw new Exception("Entity DeviceDatas or DataRawEEGs is not exist. Please check and try again.");
            var result = await _context.DataRawEEGs
                .Where(r => _context.DeviceDatas
                    .Where(x => x.DeviceId == deviceId)
                    .OrderByDescending(x => x.CreatedTime)
                    .Select(x => x.Id).Take(n)
                    .Contains(r.DeviceDataId)
                ).Select(r => new DataRawEEGVM() { Value = r.Value, RecivedTime = r.CreatedTime })
                .OrderBy(d => d.RecivedTime).AsNoTracking().ToListAsync();
            var resFormat = new DataRawEEGResponse()
            {
                Values = new List<int>(),
                RecivedTimes = new List<DateTime>()
            };
            foreach (var item in result)
            {
                resFormat.Values.Add(item.Value);
                resFormat.RecivedTimes.Add(item.RecivedTime);
            }
            return resFormat;
        }

        public (List<double> frequencyAxis, List<double> amplitudeSpectrum) GetFFTData(Guid deviceId)
        {
            var inputData = GetLastNDataRecords(deviceId, 60).Result;
            var result = Transformer.SolveFFT(inputData.Values, inputData.RecivedTimes);
            var frequencyAxis = result.frequencyAxis;
            var amplitudeSpectrum = result.amplitudeSpectrum;
            return (frequencyAxis, amplitudeSpectrum);
        }

        //public (List<int> indexs, List<double> values) GetFFT15SecData(Guid deviceId)
        public (List<double> freqs, List<double> spectrums) GetFFT15SecData(Guid deviceId)
        {
            var inputData = GetLastNDataRecords(deviceId, 15).Result.Values;
            var result = Transformer.TransformFFT(inputData);
            //var indexs = new List<int>();
            //for (int i = 0; i < result.Count; i++)
            //    indexs.Add(i);
            //return (indexs, result);
            return result;
        }

        public string GetTrainOutput()
        {
            return Classification.SVMTrain();
        }

        public List<string> GetTrainFTOutput()
        {
            return Classification.FastTreeTrain();
        }

        public AwakeStateFastTreeResponse GetTrainFTOutput(Guid deviceId)
        {
            var inputData = GetLastNDataRecords(deviceId, 15).Result.Values;
            var inputFeatures = FeaturesExtractor.ExtractAwakeState(inputData);
            return Classification.FastTreeTest(inputFeatures);
        }

        public RawEEGPredictResponse GetTrainSSAPredictOutput(Guid deviceId)
        {
            var inputData = GetLastNDataRecords(deviceId, 3).Result;
            var result = RegressionPredictor.TrainSSAWithSplitTrainTestDataSet(inputData.Values, 2*512);

            // Tính toán chuỗi thời gian tương lai gần
            var incrementsPerSecond = 512;
            var increment = TimeSpan.FromSeconds(1.0 / incrementsPerSecond);
            var timeList = new List<DateTime>((int)(2 * incrementsPerSecond));
            var startTime = inputData.RecivedTimes.Max().AddSeconds(1.0 / 2);

            for (int i = 0; i < 2 * incrementsPerSecond; i++)
            {
                timeList.Add(startTime.Add(increment * i));
            }

            return new RawEEGPredictResponse()
            {
                ForecastedValues = result.Prediction.ForecastedValues,
                LowerBoundValues = result.Prediction.LowerBoundValues,
                UpperBoundValues = result.Prediction.UpperBoundValues,
                PredictTimes = timeList,
                MAE = result.Evaluation.MAE,
                RMSE = result.Evaluation.RMSE
            };
        }
    }
}
