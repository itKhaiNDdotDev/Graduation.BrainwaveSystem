using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.DeviceData
{
    public interface IDeviceDataService : IBaseService<Models.Entities.DeviceData>
    {
        public Task<Guid>/*int*/ Create(DeviceDataRequest request);

        public Task<int> Update(Guid id, DeviceDataRequest request);

        public Task<int> Delete(Guid id);
    }
}
