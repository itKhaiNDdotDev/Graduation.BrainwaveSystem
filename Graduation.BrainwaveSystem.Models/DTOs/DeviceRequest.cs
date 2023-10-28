using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.DTOs
{
    /// <summary>
    /// DTO thông tin thiết bị do người dùng điền khi thực hiện tạo mới hoặc cập nhật.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class DeviceRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
