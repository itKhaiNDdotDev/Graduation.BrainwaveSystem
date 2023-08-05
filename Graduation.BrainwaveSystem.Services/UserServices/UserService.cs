using AutoMapper;
using Graduation.BrainwaveSystem.Models;
using Graduation.BrainwaveSystem.Models.DTOs;
using Graduation.BrainwaveSystem.Models.Entities;
using Graduation.BrainwaveSystem.Services.BaseServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.BrainwaveSystem.Services.UserServices
{
    /// <summary>
    /// Xây dựng các phương thức CRUD đơn giản nhất cho thông tin Device.
    /// </summary>
    /// Author: KhaiND (20/04/2023).
    public class UserService : BaseService<UserLogin, UserRequest>, IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context) : base(context)
        {
            _context = context;
        }


        async Task<List<UserLogin>> IUserService.GetListExist()
        {
            if (_context.UserLogins == null)
            {
                throw new Exception($"404 - Not Found: Entity set 'DataContext.Devices' is null.");
            }
            return await _context.UserLogins.Where(d => d.IsDeleted == false).ToListAsync();
        }
    }
}
