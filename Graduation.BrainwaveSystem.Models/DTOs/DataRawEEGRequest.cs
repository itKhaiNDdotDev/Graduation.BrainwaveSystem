using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    public class DataRawEEGRequest
    {
        public int Value { get; set; }

        /// <summary>
        /// ID của bản ghi dữ liệu tổng quan tương ứng làm khóa ngoại liên kết với DeviceData.
        /// </summary>
        public Guid? DeviceDataId { get; set; }
    }
}
