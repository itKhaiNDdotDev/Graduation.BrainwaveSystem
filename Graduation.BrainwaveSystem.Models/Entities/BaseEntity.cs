using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    /// <summary>
    /// Định nghĩa các thông tin dùng chung cho các Entity.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
