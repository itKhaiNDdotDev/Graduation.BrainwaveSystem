using Microsoft.AspNetCore.Mvc;
using Graduation.BrainwaveSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Graduation.BrainwaveSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Graduation.BrainwaveSystem.Models.DTOs;

namespace Graduation.BrainwaveSystem.APIs.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected readonly DataContext context;

        public UsersController(DataContext dbContext)
        {
            context = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserLogin user)
        {
            try
            {
                var check = await context.Set<UserLogin>().ToListAsync(); 
                check = check.Where(t => t.UserName == user.UserName || t.Email == user.Email).ToList();

                if (check.Count() > 0)
                {
                    return BadRequest("Username or email is already exist!");
                }
                else
                {
                    var u = new UserLogin
                    {
                        UserName = user.UserName,
                        Name = user.Name,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash),
                        PasswordSalt = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash),
                        PhotoFileName = user.PhotoFileName,
                        Id = new Guid(),
                        Role = user.Role,
                        Email = user.Email,
                        IsDeleted = false,
                        CreatedTime = DateTime.Now,
                        LastModifiedTime = DateTime.Now,
                        CreatedBy = user.CreatedBy,
                        LastModifiedBy = user.CreatedBy
                    };
                    context.Set<UserLogin>().Add(u);
                    context.SaveChanges();

                    if (user.Role != 0)
                    {
                        context.Set<UserRole>().Add(new UserRole { Id = new Guid(), UserId = u.Id, RoleId = u.Role});
                        context.SaveChanges();
                    }

                    return Ok(u);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(SignInModel s)
        {
            try
            {
                if (s.UserName != null)
                {
                    var check1 = await context.Set<UserLogin>().ToListAsync();
                    var check = check1.Where(t => t.UserName == s.UserName).FirstOrDefault();
        
                    string Token;
                    if (check == null)
                    {
                        return new JsonResult("User not Found or Wrong Password.");
                    }
                    else
                    {
                        bool bc = BCrypt.Net.BCrypt.Verify(s.Password, check.PasswordHash);
                        if (bc == false)
                        {
                            return new JsonResult("User not Found or Wrong Password.");
                        }
                        Token = CreateToken(check);
                    }

                    return Ok(new JsonResult(Token));
                }
                else return BadRequest("User not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }

        }

        private string CreateToken(UserLogin u)
        {
            var checkRole = context.Roles.Where(t=>t.Id == u.Role).FirstOrDefault();
            if (u != null && u.UserName != null && checkRole != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, u.UserName),
                    new Claim(ClaimTypes.Role, checkRole.Name.ToString()),
                    new Claim("name", u.Name),
                    new Claim("userName", u.UserName),
                    new Claim("userId", u.Id.ToString()),
                    new Claim("role", checkRole.Name.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Graduation.BrainwaveSystem"));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                    "Client",
                    "Server",
                    claims: claims,
                    expires: DateTime.Now.AddYears(1),
                    signingCredentials: cred);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            else
                return "";
        }
    }
}
