using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class DataRawEEGResponse
    {
        //public List<int>? Values { get; set; }
        public int Value { get; set; }

        public DateTime RecivedTime { get; set; }
    }
}
