using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Các giá trị raw trong 1 bản ghi dữ liệu thô thu được ở DeviceData.
    /// Quan hệ n-1 đến DeviceData.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    [Index(nameof(DeviceDataId))]
    public class DataRawEEG : BaseEntity
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        ///// <summary>
        ///// Thứ tự của giá trị raw trong 1 bản ghi DeviceData.
        ///// </summary>
        //public int Sequence { get; set; }

        /// <summary>
        /// ID của bản ghi dữ liệu tổng quan tương ứng làm khóa ngoại liên kết với DeviceData.
        /// </summary>
        public Guid DeviceDataId { get; set; }
    }
}
