using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graduation.BrainwaveSystem.Services.Base;

namespace Graduation.BrainwaveSystem.Services.DeviceData
{
    public class DeviceDataService : BaseService<Models.Entities.DeviceData>, IDeviceDataService
    {
        private readonly DataContext _context;

        public DeviceDataService(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Guid> Create(DeviceDataRequest request)
        {
            if (_context.DeviceDatas == null)
            {
                throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
            }

            var deviceData = new Models.Entities.DeviceData()
            {
                Id = new Guid(),
                PoorQuality = request.PoorQuality,
                Attention = request.Attention,
                Meditation = request.Meditation,
                DeviceId =  _context.Devices.First().Id, // Cần link đến Device và thay thế bằng ID Device tương ứng
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                CreatedBy = "KhaiND", // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
                LastModifiedTime = DateTime.Now,
                LastModifiedBy = "KhaiND" // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
            };
            _context.DeviceDatas.Add(deviceData);

            await _context.SaveChangesAsync();
            return deviceData.Id;
        }

        public async Task<int> Update(Guid id, DeviceDataRequest request)
        {
            if (_context.DeviceDatas == null) // Hơi lặp với GetById
            {
                throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
            }

            //_context.Entry(device).State = EntityState.Modified;
            var deviceData = await GetById(id);
            deviceData.PoorQuality = request.PoorQuality;
            deviceData.Attention = request.Attention;
            deviceData.Meditation = request.Meditation;
            deviceData.LastModifiedTime = DateTime.Now;
            deviceData.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

            // Có thể dùng Entry.Modified như trên
            _context.DeviceDatas.Update(deviceData);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceDataExists(id))
                {
                    throw new Exception($"404 - Not Found: DeviceData record with id [{id}] is not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<int> Delete(Guid id)
        {
            if (_context.DeviceDatas == null) // Hơi lặp với GetById
            {
                throw new Exception("404 - Not Found: Entity set 'DataContext.DeviceDatas' is null.");
            }

            var deviceData = await GetById(id);
            deviceData.IsDeleted = true;
            deviceData.LastModifiedTime = DateTime.Now;
            deviceData.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

            // Có thể dùng Entry.Modified như trên ví dụ Update
            _context.DeviceDatas.Update(deviceData);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceDataExists(id))
                {
                    throw new Exception($"404 - Not Found: DeviceData record with id [{id}] is not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool DeviceDataExists(Guid id)
        {
            return (_context.DeviceDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
