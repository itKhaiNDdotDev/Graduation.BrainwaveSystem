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

namespace Graduation.BrainwaveSystem.Services.DataRawEEGServices
{
    public class DataRawEEGService : BaseService<DataRawEEG, DataRawEEGRequest>, IDataRawEEGService
    {
        private readonly DataContext _context;

        public DataRawEEGService(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DataRawEEGResponse[]> GetLast300DataRecords(Guid deviceId)
        {
            //throw new NotImplementedException();
            var result = new DataRawEEGResponse[300];
            //Lấy Id 300 bản ghi gần nhất tương ứng thiết bị
            if (_context.DeviceDatas == null)
                throw new Exception("Entity DeviceDatas is not exist. Please check and try again.");
            var dataRecords = await _context.DeviceDatas.Where(x => x.DeviceId == deviceId)
                .OrderBy(x => x.CreatedTime)
                .Select(x => new { x.Id, x.CreatedTime })
                .TakeLast(300)
                .ToArrayAsync();

            if (_context.DataRawEEGs == null)
                throw new Exception("Entity DataRawEEGs is not exist. Please check and try again.");
            for (int i = 0; i < 300; i++)
            {
                var listValue = await _context.DataRawEEGs.Where(r => r.DeviceDataId == dataRecords[i].Id)
                    .Select(r => r.Value).ToListAsync();
                result[i] = new DataRawEEGResponse()
                {
                    Values = listValue,
                    RecivedTime = dataRecords[i].CreatedTime
                };
            }
            return result;
        }
    }
}
