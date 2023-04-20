using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Định nghĩa các thông tin Thiết bị lưu trong Database.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class Device : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Thời gian lần bắt đầu hoạt động (khởi động) gần nhất nếu đang Active.
        /// Thời gian.lần cuối cùng Active cho đến hiện tại nếu đang không Active.
        /// </summary>
        public DateTime ActiveTime { get; set; }
    }
}
