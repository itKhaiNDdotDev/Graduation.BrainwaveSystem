using Graduation.BrainwaveSystem.Cores.MLDotNETModels;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.DTOs.AIModels;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Graduation.BrainwaveSystem.Cores.MLDotNETModels.RegressionPredictor;

namespace Graduation.BrainwaveSystem.Services.DataRawEEGServices
{
    public interface IDataRawEEGService : IBaseService<DataRawEEG, DataRawEEGRequest>
    {
        //public Task<List<DataRawEEGResponse>> GetLastNDataRecords(Guid deviceId, int n = 1);
        public Task<DataRawEEGResponse> GetLastNDataRecords(Guid deviceId, int n = 1);

        public (List<double> frequencyAxis, List<double> amplitudeSpectrum) GetFFTData(Guid deviceId);
        public (List<double> freqs, List<double> spectrums) GetFFT15SecData(Guid deviceId);

        public string GetTrainOutput();

        public List<string> GetTrainFTOutput();

        public AwakeStateFastTreeResponse GetTrainFTOutput(Guid deviceId);

        public (TimeSeriesPredictMetricsModel Evaluation, ModelOutput Prediction) GetTrainSSAPredictOutput(Guid deviceId);
    }
}