using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
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
    public interface IDeviceService
    {
        public Task<List<Models.Entities.Device>> GetAll();

        public Task<Models.Entities.Device> GetById(Guid id);

        public Task<Guid>/*int*/ Create(DeviceRequest request);

        public Task<int> Update(Guid id, DeviceRequest request);

        public Task<int> Delete(Guid id);

        public Task<int> DeleteForever(Guid id);
    }
}
