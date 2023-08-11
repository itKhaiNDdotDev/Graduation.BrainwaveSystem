using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
