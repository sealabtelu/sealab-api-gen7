using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;
using System.Net;
using System.Security.Claims;

namespace SealabAPI.DataAccess.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<object> Login(string username, string password);
        Task ChangePassword(ChangePasswordRequest model);
    }
    public class UserService : BaseService<User>, IUserService
    {
        private readonly SeelabsService _seelabsService;
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
                    throw new HttpRequestException("Wrong password!", null, HttpStatusCode.Unauthorized);

                SeelabsLoginResponse seelabs = await _seelabsService.Login(new SeelabsLoginRequest(user.Nim, password, user.Role));

                user.AppToken = JwtHelper.CreateToken(new Claim[]{
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString()),
                    seelabs.Valid? new Claim("seelabs_token", seelabs.Token) : null
                }, 2);

                await _appDbContext.SaveChangesAsync();

                if (result.Assistants != null)
                {
                    LoginAssistantResponse model = new();
                    model.MapToModel(result.Assistants);
                    model.MapToModel(user);
                    model.IdAssistant = result.Assistants.Id;
                    userDetails = model;
                }
                else if (result.Students != null)
                {
                    LoginStudentResponse model = new();
                    model.MapToModel(result.Students);
                    model.MapToModel(user);
                    model.IdStudent = result.Students.Id;
                    userDetails = model;
                }

                userDetails.Seelabs = seelabs.Valid ? "Valid" : "Invalid";
            }
            else
            {
                throw new HttpRequestException("Username not found!", null, HttpStatusCode.NotFound);
            }

            return userDetails;
        }
        public async Task ChangePassword(ChangePasswordRequest model)
        {
            User user = await _appDbContext.Set<User>().FindAsync(model.IdUser);
            if (!PasswordHelper.VerifyHashedPassword(user.Password, model.OldPassword))
                throw new HttpRequestException("Wrong password!", null, HttpStatusCode.Unauthorized);
            user.Password = model.NewPassword.HashPassword();
            await _appDbContext.SaveChangesAsync();
        }
    }
}
