using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class DeviceDataRequest
    {
        public int? PoorQuality { get; set; }

        public int? Attention { get; set; }

        public int? Meditation { get; set; }

        public Guid? DeviceId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
