using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation.BrainwaveSystem.Models.Entities
{
    [Table("UserLogins")]
    public class UserLogin : BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Chưa nhập Họ và tên")]
        [Column("Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên tài khoản")]
        [Column("UserName")]
        [MinLength(6, ErrorMessage = "Nhập tài khoản nhất 6 kí tự")]
        public string? UserName { get; set; }

        [MinLength(6, ErrorMessage = "Nhập tài khoản nhất 6 kí tự")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [Column("PasswordHash")]
        public string? PasswordHash { get; set; }

        [MinLength(6, ErrorMessage = "Nhập tài khoản nhất 6 kí tự")]
        [Required(ErrorMessage = "Chưa nhập xác nhận mật khẩu")]
        [Column("PasswordSalt")]
        public string? PasswordSalt { get; set; }

        [Column("PhotoFileName")]
        public string? PhotoFileName { get; set; }

        [Required(ErrorMessage = "Chưa phân quyền cho tài khoản")]
        [Column("Role")]
        public int Role { get; set; } = 0;

        [StringLength(50)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Cấu trúc email không đúng !")]
        [Required(ErrorMessage = "Vui lòng nhập email để xác nhận tài khoản")]
        [Column("Email")]
        public string? Email { get; set; }
    }
}
