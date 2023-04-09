using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; } // THời gian cụ thể có cả giờ phút giây
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } // THời gian cụ thể có cả giờ phút giây
        public string LastModifiedBy { get; set; }
    }
}
