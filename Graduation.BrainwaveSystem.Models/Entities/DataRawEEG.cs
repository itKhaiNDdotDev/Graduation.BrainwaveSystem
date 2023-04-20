using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Các giá trị raw trong 1 bản ghi dữ liệu thô thu được ở DeviceData.
    /// Quan hệ n-1 đến DeviceData.
    /// </summary>
    /// Author: KhaiND (20/04/2023)
    public class DataRawEEG
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        ///// <summary>
        ///// Thứ tự của giá trị raw trong 1 bản ghi DeviceData
        ///// </summary>
        //public int Sequence { get; set; }

        public Guid DeviceDataId { get; set; }
    }
}
