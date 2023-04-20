using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Dữ liệu 8 dải (cột) sóng não mà thiết bị tổng hợp được.
    /// Quan hệ 1-1 đến DeviceData.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
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
        /// Ultra-high frequency Gamma.
        /// </summary>
        public int UHFGamma { get; set; }

        /// <summary>
        /// ID của bản ghi dữ liệu tổng quan tương ứng làm khóa ngoại liên kết với DeviceData.
        /// </summary>
        public Guid DeviceDataId { get; set; }
    }
}
