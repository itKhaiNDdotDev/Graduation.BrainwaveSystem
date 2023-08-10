using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs.ViewModels
{
    public class DataRawEEGVM
    {
        public int Value { get; set; }
        public DateTime RecivedTime { get; set; }
    }
}
