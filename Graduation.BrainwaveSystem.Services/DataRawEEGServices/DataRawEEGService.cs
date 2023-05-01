using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            var dataRecords = (List<DataMapRecord>) await _context.DeviceDatas.Where(x => x.DeviceId == deviceId)
                .Select(x => new DataMapRecord { Id = x.Id, CreatedTime = x.CreatedTime })
                .OrderByDescending(x => x.CreatedTime)
                .Take(300).ToListAsync();

            

            var arrayDataRecord = new DataMapRecord[300];
            for (int i = 0; i < dataRecords.Count; i++)
            {
                if (i >= 300)
                {
                    break;
                }
                arrayDataRecord[i] = dataRecords[i];
            }

            if (dataRecords.Count < 300)
            {
                for (int i = dataRecords.Count; i < 300; i++)
                {
                    arrayDataRecord[i] = new DataMapRecord{ Id = Guid.Empty, CreatedTime = arrayDataRecord[i-1].CreatedTime.AddSeconds(-1) };
                }
            }

            arrayDataRecord = arrayDataRecord.OrderBy(r => r.CreatedTime).ToArray();

            if (_context.DataRawEEGs == null)
                throw new Exception("Entity DataRawEEGs is not exist. Please check and try again.");
            for (int i = 0; i < 300; i++)
            {
                var listValue = await _context.DataRawEEGs.Where(r => r.DeviceDataId == arrayDataRecord[i].Id)
                    .Select(r => r.Value).ToListAsync();
                result[i] = new DataRawEEGResponse()
                {
                    Values = listValue ?? new List<int>(),
                    RecivedTime = arrayDataRecord[i].CreatedTime
                };
            }
            return result;
        }

        class DataMapRecord
        {
            internal Guid? Id { get; set; }
            internal DateTime CreatedTime { get; set; }
        }
    }
}
