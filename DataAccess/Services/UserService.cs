using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;
using System.Security.Claims;

namespace SealabAPI.DataAccess.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<object> Login(string username, string password);
    }
    public class UserService : BaseService<User>, IUserService
    {
        private SeelabsService _seelabsService;
        public UserService(AppDbContext appDbContext, SeelabsService seelabsService) : base(appDbContext)
        {
            _seelabsService = seelabsService;
        }
        public async Task<object> Login(string username, string password)
        {

            dynamic userDetails = null;

            var query = from u in await _appDbContext.Set<User>().ToListAsync()
                        join a in await _appDbContext.Set<Assistant>().ToListAsync() on u.Id equals a.IdUser into assistant
                        join s in await _appDbContext.Set<Student>().ToListAsync() on u.Id equals s.IdUser into student
                        where u.Username == username || u.Nim == username
                        select new
                        {
                            User = u,
                            Assistants = assistant.FirstOrDefault(),
                            Students = student.FirstOrDefault()
                        };

            var result = query.FirstOrDefault();

            if (result != null)
            {
                User user = result.User;
                if (!PasswordHelper.VerifyHashedPassword(user.Password, password))
                    throw new ArgumentException("Wrong Password");

                var seelabs = _seelabsService.Login(user.Nim, password, user.Role);

                user.AppToken = JwtHelper.CreateToken(new Claim[]{
                    // new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    seelabs.valid? new Claim("seelabs_token", seelabs.token) : null
                }, DateTime.UtcNow.AddHours(2));

                await _appDbContext.SaveChangesAsync();

                if (result.Assistants != null)
                {
                    LoginAssistantResponse model = new();
                    model.MapToModel(result.Assistants);
                    model.MapToModel(user);
                    userDetails = model;
                }
                else if (result.Students != null)
                {
                    LoginStudentResponse model = new();
                    model.MapToModel(result.Students);
                    model.MapToModel(user);
                    userDetails = model;
                }

                userDetails.Seelabs = seelabs.valid ? "Valid" : "Invalid";
            }

            return userDetails;
        }
    }
}
