using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
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
    }
}
