using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class LoginRequest : BaseModel
    {
        private string _username;
        public string Username { get => _username; set => _username = value?.ToLower(); }
        public string Password { get; set; }
    }
    public class LoginRequestValidator : AbstractModelValidator<LoginRequest>
    {
        public LoginRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                User user = dbContext.Set<User>().AsNoTracking().FirstOrDefault(x => x.Username == data.Username);

                if (user == null)
                    context.AddFailure("username", "Username not found!");
                if (!PasswordHelper.VerifyHashedPassword(user.Password, data.Password))
                    context.AddFailure("password", "Wrong password!");
            });
        }
    }
}
