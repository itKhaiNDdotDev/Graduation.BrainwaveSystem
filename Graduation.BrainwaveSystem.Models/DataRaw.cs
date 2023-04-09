using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models
{
    public class DataRaw : BaseEntity // Map n-1 to DeviceData
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        //public int Sequence { get; set; }
        public Guid DeviceDataId { get; set; }
    }
}