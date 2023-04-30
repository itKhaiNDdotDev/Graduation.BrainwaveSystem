using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Graduation.BrainwaveSystem.Services.DataRawEEGServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.Data8BandsEEGServices
{
    public class Data8BandsEEGService : BaseService<Data8BandsEEG, Data8BandsEEGRequest>, IData8BandsEEGService
    {
        private readonly DataContext _context;

        public Data8BandsEEGService(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
