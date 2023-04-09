using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models
{
    public class DataEEG8Band : BaseEntity // map n-1 to DeviceData
    {
        public Guid Id { get; set; }
        public int Delta { get; set; }
        public int Theta { get; set; }
        public int Alpha { get; set; }
        public int LowBeta { get; set; }
        public int MidBeta { get; set; }
        public int HighBeta { get; set; }
        public int Gamma { get; set; }
        public int UHFGamma { get; set; } //Ultra-high frequency Gamma
        public Guid DeviceDataId { get; set; }
    }
}
