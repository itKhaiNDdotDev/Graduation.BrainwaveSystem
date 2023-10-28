using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class Data8BandsEEGRequest
    {
        public int? Delta { get; set; }

        public int? Theta { get; set; }

        public int? Alpha { get; set; }

        public int? LowBeta { get; set; }

        public int? MidBeta { get; set; }

        public int? HighBeta { get; set; }

        public int? Gamma { get; set; }

        /// <summary>
        /// Ultra-high frequency Gamma.
        /// </summary>
        public int? UHFGamma { get; set; }

        /// <summary>
        /// ID của bản ghi dữ liệu tổng quan tương ứng làm khóa ngoại liên kết với DeviceData.
        /// </summary>
        public Guid? DeviceDataId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
