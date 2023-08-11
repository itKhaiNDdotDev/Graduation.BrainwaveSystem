using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class DataDeviceSendRequest
    {
        public DeviceDataRequest? General { get; set; }

        //public Data8BandsEEGRequest? Data8BandsEEG { get; set; }
        public int? Delta { get; set; }
        public int? Theta { get; set; }
        public int? Alpha { get; set; }
        public int? LowBeta { get; set; }
        public int? MidBeta { get; set; }
        public int? HighBeta { get; set; }
        public int? Gamma { get; set; }
        public int? UHFGamma { get; set; }

        //public List<DataRawEEGRequest>? DataRawEEGs { get; set; }
        public List<int>? RawEEGs { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
