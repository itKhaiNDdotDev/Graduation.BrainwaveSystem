using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    public class Data8BandsEEG : BaseEntity
    {
        public Guid Id { get; set; }

        public int Delta { get; set; }

        public int Theta { get; set; }

        public int Alpha { get; set; }

        public int LowBeta { get; set; }

        public int MidBeta { get; set; }

        public int HighBeta { get; set; }

        public int Gamma { get; set; }

        /// <summary>
        /// Ultra-high frequency Gamma
        /// </summary>
        public int UHFGamma { get; set; }

        public Guid DeviceDataId { get; set; }
    }
}
