using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Các thông tin cơ bản thu được từ một bản ghi dữ liệu thô
    /// </summary>
    /// Author: KhaiND (20/04/2023)
    public class DeviceData : BaseEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Dữ liệu chất lượng thấp thu được tính từ 0 đến 255
        /// </summary>
        public int PoorQuality { get; set; }

        /// <summary>
        /// Dữ liệu mức độ tập trung thu được tính giá trị tư 0 đến 255
        /// </summary>
        public int Attention { get; set; }

        /// <summary>
        /// Dữ liệu mức độ thư giãn (thiền) tính giá trị tư 0 đến 255
        /// </summary>
        public int Meditation { get; set; }

        /// <summary>
        /// ID của thiết bị làm khóa ngoại liên kết với Device
        /// </summary>
        public Guid DeviceId { get; set; }
    }
}
