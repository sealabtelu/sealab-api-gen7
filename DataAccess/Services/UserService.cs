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
        private readonly SeelabsPracticumService _practicumService;
        private readonly SeelabsProctorService _proctorService;
        public UserService(AppDbContext appDbContext, SeelabsPracticumService practicumService, SeelabsProctorService proctorService) : base(appDbContext)
        {
            _practicumService = practicumService;
            _proctorService = proctorService;
        }
        public async Task<object> Login(string username, string password)
        {
            User user = await _appDbContext.Set<User>().Include(x => x.Assistant).Include(x => x.Student).FirstOrDefaultAsync(x => x.Username == username);
            dynamic userDetails = null;

            if (user != null)
            {
                if (!PasswordHelper.VerifyHashedPassword(user.Password, password))
                    throw new HttpRequestException("Wrong password!", null, HttpStatusCode.Unauthorized);

                SeelabsLoginRequest seelabsLoginRequest = new(user.Nim, password, user.Role);
                SeelabsLoginResponse practicum = await _practicumService.Login(seelabsLoginRequest);

                List<Claim> claims = new()
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString())
                };

                if (practicum.Valid) claims.Add(new Claim("practicum_token", practicum.Token));

                if (user.Assistant != null)
                {
                    SeelabsLoginResponse proctor = await _proctorService.Login(seelabsLoginRequest);
                    if (proctor.Valid) claims.Add(new Claim("proctor_token", proctor.Token));

                    LoginAssistantResponse model = new();
                    user.AppToken = JwtHelper.CreateToken(claims.ToArray(), 3);
                    model.IdAssistant = user.Assistant.Id;
                    model.MapToModel(user.Assistant);
                    model.MapToModel(user);
                    userDetails = model;
                    userDetails.Seelabs = new
                    {
                        practicum = practicum.Valid ? "Valid" : "Invalid",
                        proctor = proctor.Valid ? "Valid" : "Invalid"
                    };
                }
                else if (user.Student != null)
                {
                    LoginStudentResponse model = new();
                    user.AppToken = JwtHelper.CreateToken(claims.ToArray(), 3);
                    model.IdStudent = user.Student.Id;
                    model.MapToModel(user.Student);
                    model.MapToModel(user);
                    userDetails = model;
                    userDetails.Seelabs = practicum.Valid ? "Valid" : "Invalid";
                }

                await _appDbContext.SaveChangesAsync();
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