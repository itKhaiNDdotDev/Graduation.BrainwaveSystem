
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;

namespace Graduation.BrainwaveSystem.Services.UserServices
{
    /// <summary>
    /// Khai báo các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public interface IUserService : IBaseService<UserLogin, UserRequest>
    {
        public Task<List<UserLogin>> GetListExist();
    }
}
