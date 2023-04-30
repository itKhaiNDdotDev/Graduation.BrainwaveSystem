using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.DeviceServices
{
    /// <summary>
    /// Khai báo các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public interface IDeviceService : IBaseService<Device, DeviceRequest>
    {
        //public Task<Guid>/*int*/ Create(DeviceRequest request);

        //public Task<int> Update(Guid id, DeviceRequest request);

        //public Task<int> Delete(Guid id);
    }
}
