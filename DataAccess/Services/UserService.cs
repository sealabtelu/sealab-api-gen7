using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        Task<string> GenerateEmailVerificationOTP(string email);
    }
    public class UserService : BaseService<User>, IUserService
    {
        private readonly SeelabsPracticumService _practicumService;
        private readonly SeelabsProctorService _proctorService;
        private readonly IMailService _mailService;
        private readonly IMemoryCache _memoryCache;
        public UserService(AppDbContext appDbContext, SeelabsPracticumService practicumService, SeelabsProctorService proctorService, IMailService mailService, IMemoryCache memoryCache) : base(appDbContext)
        {
            _practicumService = practicumService;
            _proctorService = proctorService;
            _mailService = mailService;
            _memoryCache = memoryCache;
        }
        private string CreateOtp(string email)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            _memoryCache.Set(email, otp, TimeSpan.FromMinutes(10));
            return otp;
        }
        // public async Task<string> GeneratePasswordResetOTP(string email)
        // {
        //     User user = await _appDbContext.Set<User>().Include(x => x.UserData).FirstOrDefaultAsync(x => x.Email == email) ??
        //         throw new HttpRequestException("Email not found!", null, HttpStatusCode.NotFound);
        //     if (user.UserData == null)
        //         throw new HttpRequestException("User data not found!", null, HttpStatusCode.NotFound);
        //     if (!user.IsVerified)
        //         throw new ArgumentException("Email not verified!");
        //     string otp = CreateOtp(email);
        //     string bodyHtml = await MailHelper.ComposeResetPasswordHTML(email, user.UserData.Username, otp);
        //     await MailHelper.SendEmail(email, "Password Reset Verification Code", bodyHtml);
        //     return otp;
        // }
        public async Task<string> GenerateEmailVerificationOTP(string email)
        {
            User user = await _appDbContext.Set<User>().Include(x => x.Student).Include(x => x.Assistant).FirstOrDefaultAsync(x => x.Email == email) ??
                throw new HttpRequestException("Email not found!", null, HttpStatusCode.NotFound);
            if (user.IsVerified)
                throw new ArgumentException("Email is verified!");
            string otp = CreateOtp(email);
            string bodyHtml = await _mailService.ComposeVerifyEmailHTML(email, user.Username, otp);
            await _mailService.SendEmail(email, "Email Address Verification", bodyHtml);
            return otp;
        }
        // public async Task<string> VerifyEmailOTP(string email, string code)
        // {
        //     if (_memoryCache.TryGetValue(email, out string storedOtp))
        //     {
        //         if (storedOtp == code)
        //         {
        //             User user = await _appDbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        //             user.IsVerified = true;
        //             await _appDbContext.SaveChangesAsync();
        //             return "User Verified!";
        //         }
        //     }
        //     throw new ArgumentException("Verification failed try again!");
        // }
        // public async Task<string> VerifyPasswordOTP(VerifyPasswordRequest request)
        // {
        //     if (_memoryCache.TryGetValue(request.Email, out string storedOtp))
        //     {
        //         if (storedOtp == request.Code)
        //         {
        //             User user = await _appDbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == request.Email);
        //             user.Password = request.Password.HashPassword();
        //             await _appDbContext.SaveChangesAsync();
        //             return "Password Changed!";
        //         }
        //     }
        //     throw new HttpRequestException("Invalid Verification code or email!", null, HttpStatusCode.NotFound);
        // }
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