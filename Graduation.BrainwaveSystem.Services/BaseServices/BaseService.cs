using AutoMapper;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graduation.BrainwaveSystem.Services.BaseServices
{
    public class BaseService<T, TRequest> : IBaseService<T, TRequest> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> Ts;

        public BaseService(DataContext context)
        {
            _context = context;
            Ts = _context.Set<T>();
            //Ts = _context.Model.FindEntityType(typeof(T)).;
        }

        public async Task<List<T>> GetAll()
        {
            if (Ts == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.{typeof(T).Name}s' is null.");
            }
            return await Ts.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            if (Ts == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.{typeof(T).Name}s' is null.");
            }
            var record = await Ts.FindAsync(id);

            if (record == null)
            {
                throw new Exception($"404 - Not Found: {typeof(T).Name} record with id [{id}] is not exist.");
            }
            return record;
        }

        public async Task<Guid> Create(TRequest request)
        {
            if (Ts == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.{typeof(T).Name}s' is null.");
            }

            //T? item = (T?)Convert.ChangeType(request, typeof(T));
            //T? item = request as T;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRequest, T>();
                //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => ""));
            });
            IMapper mapper = config.CreateMapper();
            T item = mapper.Map<T>(request);

            var properties = typeof(T).GetProperties();
            var newId = Guid.NewGuid();
            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case "Id":
                        property.SetValue(item, newId);
                        break;
                    case "IsDeleted":
                        property.SetValue(item, false);
                        break;
                    case "CreatedTime":
                        property.SetValue(item, DateTime.Now);
                        break;
                    case "LastModifiedTime":
                        property.SetValue(item, DateTime.Now);
                        break;
                    default:
                        //if(property.Name == "IsActive")
                        //{
                        //    property.SetValue(item, request.IsActive.HasValue ? request.IsActive.Value : true);
                        //}
                        if (property.Name == "ActiveTime")
                        {
                            property.SetValue(item, DateTime.Now);
                        }
                        break;
                }
            }
            if (item == null)
            {
                throw new InvalidCastException($"Invalid Input Data: It can not be a {typeof(T).Name} record. Please check and try again!");
            }
            Ts.Add(item); // Can xem lai cho nay
            await _context.SaveChangesAsync();
            return newId;
        }

        public async Task<int> Update(Guid id, TRequest request)
        {
            //_context.Entry(device).State = EntityState.Modified;
            var item = await GetById(id);
            //T? reqItem = (T?)Convert.ChangeType(request, typeof(T));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRequest, T>()
                .ForAllMembers(opt => opt.Condition((src, dst, val) => val != null));
                //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => ""));
            });
            IMapper mapper = config.CreateMapper();
            //T reqItem = mapper.Map<T>(request);

            //if (reqItem == null)
            //    return 0;

            //var reqPropIsActive = request!.GetType().GetProperty("IsActive");
            //if (reqPropIsActive != null && reqPropIsActive.GetValue(request, null) != null)
            //{
            //    bool oldValue = (bool) item.GetType().GetProperty("IsActive")!.GetValue(item, null)!;//device.IsActive;
            //    bool newValue = (bool) reqPropIsActive.GetValue(request, null)!;//request.IsActive.Value;

            //    if (oldValue != newValue)
            //    {
            //        //device.IsActive = newValue;
            //        reqItem.GetType().GetProperty("IsActive")!.SetValue(item, newValue);
            //        //device.ActiveTime = DateTime.Now;
            //        reqItem.GetType().GetProperty("ActiveTime")!.SetValue(item, DateTime.Now);
            //    }
            //}
            ////device.LastModifiedTime = DateTime.Now;
            //reqItem.GetType().GetProperty("LastModifiedTime")!.SetValue(item, DateTime.Now);
            ////device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
            //reqItem.GetType().GetProperty("LastModifiedBy")!.SetValue(item, "KhaiND");
            //item = reqItem;

            bool oldValue, newValue;
            var reqPropIsActive = request!.GetType().GetProperty("IsActive");
            if (reqPropIsActive != null && reqPropIsActive.GetValue(request, null) != null)
            {
                oldValue = (bool)item.GetType().GetProperty("IsActive")!.GetValue(item, null)!;//device.IsActive;
                newValue = (bool)reqPropIsActive.GetValue(request, null)!;//request.IsActive.Value;

                if (oldValue != newValue)
                {
                    //device.IsActive = newValue;
                    item.GetType().GetProperty("IsActive")!.SetValue(item, newValue);
                    //device.ActiveTime = DateTime.Now;
                    item.GetType().GetProperty("ActiveTime")!.SetValue(item, DateTime.Now);
                }
            }
            item = mapper.Map(request, item);
            //device.LastModifiedTime = DateTime.Now;
            item.GetType().GetProperty("LastModifiedTime")!.SetValue(item, DateTime.Now);
            //device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.
            item.GetType().GetProperty("LastModifiedBy")!.SetValue(item, "KhaiND");

            // Có thể dùng Entry.Modified như trên
            Ts.Update(item);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    throw new Exception($"404 - Not Found: {typeof(T).Name} record with id [{id}] is not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        public virtual async Task<int> Delete(Guid id)
        {
            var item = await GetById(id);
            item.GetType().GetProperty("ActiveTime")!.SetValue(item, DateTime.Now); //device.ActiveTime = DateTime.Now;
            item.GetType().GetProperty("IsDeleted")!.SetValue(item, true); //device.IsDeleted = true;
            item.GetType().GetProperty("LastModifiedTime")!.SetValue(item, DateTime.Now); //device.LastModifiedTime = DateTime.Now;
            item.GetType().GetProperty("LastModifiedBy")!.SetValue(item, "KhaiND"); //device.LastModifiedBy = "KhaiND"; // Cần thay thế bằng tên tương ứng Profile khi có Authentication.

            // Có thể dùng Entry.Modified như trên ví dụ Update
            Ts.Update(item); //_context.Devices.Update(device);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    throw new Exception($"404 - Not Found: {typeof(T).Name} record with id [{id}] is not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        public virtual async Task<int> DeleteForever(Guid id)
        {
            var record = await GetById(id);

            Ts.Remove(record);
            return await _context.SaveChangesAsync();
        }

        private bool EntityExists(Guid id)
        {
            return (Ts?.Any(e => EF.Property<Guid>(e, "Id") == id)).GetValueOrDefault();
        }
    }
}