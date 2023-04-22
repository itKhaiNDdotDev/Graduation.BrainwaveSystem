using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.Device
{
    /// <summary>
    /// Khai báo các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public interface IDeviceService : IBaseService<Models.Entities.Device>
    {
        public Task<Guid>/*int*/ Create(DeviceRequest request);

        public Task<int> Update(Guid id, DeviceRequest request);

        public Task<int> Delete(Guid id);
    }
}
