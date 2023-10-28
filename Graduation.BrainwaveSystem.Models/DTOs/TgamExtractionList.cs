using Graduation.BrainwaveSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class TgamExtractionList
    {
        public List<DeviceData>? Generals { get; set; }

        public List<Data8BandsEEG>? Data8Bands { get; set; }
    }
}
