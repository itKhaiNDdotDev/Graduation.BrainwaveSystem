using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.Device
{
    /// <summary>
    /// Xây dựng các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class DeviceService : IDeviceService
    {
        private readonly DataContext _context;

        public DeviceService(DataContext context)
        {
            _context = context;
        }

        public List<Models.Entities.Device>? GetAll()
        {
            if (_context.Devices == null)
            {
                return null;
            }
            //return await _context.Devices.ToListAsync();
            return _context.Devices.ToList();
        }

        public Models.Entities.Device? GetById(Guid id)
        {
            if (_context.Devices == null)
            {
                return null;
            }
            //var device = await _context.Devices.FindAsync(id);
            var device = _context.Devices.FirstOrDefault(x => x.Id == id);

            if (device == null)
            {
                return null;
            }
            return device;
        }

        public int Create(DeviceRequest request)
        {
            if (_context.Devices == null)
            {
                return -2; //Problem("Entity set 'DataContext.Device'  is null.");
            }

            var device = new Models.Entities.Device()
            {
                Id = new Guid(),
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive.HasValue ? request.IsActive.Value : true,
                ActiveTime = DateTime.Now,
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                CreatedBy = "KhaiND", // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
                LastModifiedTime = DateTime.Now,
                LastModifiedBy = "KhaiND" // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
            };
            _context.Devices.Add(device);
            //await _context.SaveChangesAsync();
            return _context.SaveChanges();
        }

        public int Update(Guid id, DeviceRequest request)
        {
            //_context.Entry(device).State = EntityState.Modified;
            var device = GetById(id);
            if(device == null)
                return -1; // Record not exist
            device.Name = request.Name;
            device.Description = request.Description;
            if(request.IsActive.HasValue)
            {
                bool oldValue = device.IsActive;
                bool newValue = request.IsActive.Value;

                if(oldValue != newValue)
                {
                    device.IsActive = newValue;
                    device.ActiveTime = DateTime.Now;
                }   
            }
            device.LastModifiedTime = DateTime.Now;
            device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

            _context.Devices.Update(device); // Có thể dùng Entry.Modified như trên
            try
            {
                //await _context.SaveChangesAsync();
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DeviceExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }
        }

        public int Delete(Guid id)
        {
            var device = GetById(id);
            if (device == null)
                return -1; // Record not exist

            device.ActiveTime = DateTime.Now;
            device.IsDeleted = true;
            device.LastModifiedTime = DateTime.Now;
            device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

            _context.Devices.Update(device); // Có thể dùng Entry.Modified như trên
            try
            {
                //await _context.SaveChangesAsync();
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DeviceExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }
        }

        public int DeleteForever(Guid id)
        {
            if (_context.Devices == null)
            {
                return -2; //Problem("Entity set 'DataContext.Device'  is null.");
            }
            //var device = await _context.Devices.FindAsync(id);
            var device = GetById(id);
            if (device == null)
            {
                return -1;
            }

            _context.Devices.Remove(device);
            //await _context.SaveChangesAsync();
            return _context.SaveChanges();
        }

        private bool DeviceExists(Guid id)
        {
            return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
