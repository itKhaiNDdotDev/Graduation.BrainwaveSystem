using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.DTOs.AIModels;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.DeviceDataServices
{
    public interface IDeviceDataService : IBaseService<DeviceData, DeviceDataRequest>
    {
        //public Task<Guid>/*int*/ Create(DeviceDataRequest request);

        //public Task<int> Update(Guid id, DeviceDataRequest request);

        //public Task<int> Delete(Guid id);

        public Task<(List<DeviceData> GeneralExtractions, List<Data8BandsEEG> Data8Bands)> GetLastNRecords(Guid deviceId, int n = 1);

        public Task<Guid> Create(Guid deviceId, DataDeviceSendRequest request);

        public TgamExtractionPredictResponse GetTrainSSAPredictOutput(Guid deviceId);
    }
}
