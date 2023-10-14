using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreateStudentRequest : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
    }
    public class CreateStudentRequestValidator : AbstractModelValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                var user = dbContext.Set<User>().AsNoTracking();
                if (user.Any(x => x.Username == data.Username))
                    context.AddFailure("username", "Username exist!");
                if (user.Any(x => x.Nim == data.Nim))
                    context.AddFailure("nim", "NIM exist!");
                if (user.Any(x => x.Nim == data.Phone))
                    context.AddFailure("phone", "Phone number registered!");
            });
        }
    }
}
