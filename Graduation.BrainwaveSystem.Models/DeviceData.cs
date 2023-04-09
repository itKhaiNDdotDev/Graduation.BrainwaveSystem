using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models
{
    public class DeviceData : BaseEntity
    {
        public Guid Id { get; set; }
        public int PoorQuality { get; set; }
        public int Attention { get; set; }
        public int Meditation { get; set; }
    }
}
