using AutoMapper;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.DeviceServices
{
    /// <summary>
    /// Xây dựng các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class DeviceService : BaseService<Device, DeviceRequest>, IDeviceService
    {
        private readonly DataContext _context;

        public DeviceService(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Device>> GetListExist()
        {
            if (_context.Devices == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.Devices' is null.");
            }
            return await _context.Devices.Where(d => d.IsDeleted == false).ToListAsync();
        }

        public async Task ToggleActive(Guid id)
        {
            var item = await GetById(id);
            item.IsActive = !item.IsActive;
            if(item.IsActive)
            {
                item.ActiveTime = DateTime.Now;
            }
            item.LastModifiedTime = DateTime.Now;
            item.LastModifiedBy = "KhaiND";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        //public async Task<Guid> Create(DeviceRequest request)
        //{
        //    if (_context.Devices == null)
        //    {
        //        throw new Exception("404 - Not Found: Entity set 'DataContext.Devices' is null.");
        //    }

        //    var device = new Device()
        //    {
        //        Id = new Guid(),
        //        Name = request.Name,
        //        Description = request.Description,
        //        UserId = Guid.Empty, // Cần thay thế bằng Id tương ứng Profile khi có Authentication.
        //        IsActive = request.IsActive.HasValue ? request.IsActive.Value : true,
        //        ActiveTime = DateTime.Now,
        //        IsDeleted = false,
        //        CreatedTime = DateTime.Now,
        //        CreatedBy = "KhaiND", // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
        //        LastModifiedTime = DateTime.Now,
        //        LastModifiedBy = "KhaiND" // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
        //    };
        //    _context.Devices.Add(device);

        //    await _context.SaveChangesAsync();
        //    return device.Id;
        //}

        //public async Task<int> Update(Guid id, DeviceRequest request)
        //{
        //    //_context.Entry(device).State = EntityState.Modified;
        //    var device = await GetById(id);
        //    device.Name = request.Name;
        //    device.Description = request.Description;
        //    if (request.IsActive.HasValue)
        //    {
        //        bool oldValue = device.IsActive;
        //        bool newValue = request.IsActive.Value;

        //        if (oldValue != newValue)
        //        {
        //            device.IsActive = newValue;
        //            device.ActiveTime = DateTime.Now;
        //        }
        //    }
        //    device.LastModifiedTime = DateTime.Now;
        //    device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

        //    // Có thể dùng Entry.Modified như trên
        //    _context.Devices.Update(device);
        //    try
        //    {
        //        return await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DeviceExists(id))
        //        {
        //            throw new Exception($"404 - Not Found: Device record with id [{id}] is not exist.");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //public async Task<int> Delete(Guid id)
        //{
        //    var device = await GetById(id);
        //    device.ActiveTime = DateTime.Now;
        //    device.IsDeleted = true;
        //    device.LastModifiedTime = DateTime.Now;
        //    device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

        //    // Có thể dùng Entry.Modified như trên ví dụ Update
        //    _context.Devices.Update(device);
        //    try
        //    {
        //        return await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DeviceExists(id))
        //        {
        //            throw new Exception($"404 - Not Found: Device record with id [{id}] is not exist.");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //private bool DeviceExists(Guid id)
        //{
        //    return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
