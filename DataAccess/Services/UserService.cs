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
            User user = await _appDbContext.Set<User>().Include(x => x.Assistant).Include(x => x.Student).FirstOrDefaultAsync(x => x.Username == username);
            dynamic userDetails = null;

            if (user != null)
            {
                if (!PasswordHelper.VerifyHashedPassword(user.Password, password))
                    throw new HttpRequestException("Wrong password!", null, HttpStatusCode.Unauthorized);

                SeelabsLoginResponse seelabs = await _seelabsService.Login(new SeelabsLoginRequest(user.Nim, password, user.Role));

                user.AppToken = JwtHelper.CreateToken(new Claim[]{
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString()),
                    seelabs.Valid? new Claim("seelabs_token", seelabs.Token) : null
                }, 2);

                await _appDbContext.SaveChangesAsync();

                if (user.Assistant != null)
                {
                    LoginAssistantResponse model = new();
                    model.MapToModel(user.Assistant);
                    model.MapToModel(user);
                    model.IdAssistant = user.Assistant.Id;
                    userDetails = model;
                }
                else if (user.Student != null)
                {
                    LoginStudentResponse model = new();
                    model.MapToModel(user.Student);
                    model.MapToModel(user);
                    model.IdStudent = user.Student.Id;
                    userDetails = model;
                }

                userDetails.Seelabs = seelabs.Valid ? "Valid" : "Invalid";
            }
            else
                throw new HttpRequestException("Username not found!", null, HttpStatusCode.NotFound);

            return userDetails;
        }
        public async Task ChangePassword(ChangePasswordRequest model)
        {
            User user = await _appDbContext.Set<User>().FindAsync(model.IdUser);
            if (!PasswordHelper.VerifyHashedPassword(user.Password, model.OldPassword))
                throw new HttpRequestException("Wrong old password!", null, HttpStatusCode.Unauthorized);
            user.Password = model.NewPassword.HashPassword();
            await _appDbContext.SaveChangesAsync();
        }
    }
}
