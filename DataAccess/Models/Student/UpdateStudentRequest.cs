using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateStudentRequest : BaseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
    }
    public class UpdateStudentRequestValidator : AbstractModelValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                var user = dbContext.Set<User>().AsNoTracking();
                Student student = dbContext.Set<Student>().Include(x => x.User).Where(s => s.Id == data.Id).AsNoTracking().FirstOrDefault();
                if (user.Any(x => x.Username == data.Username && x.Id != student.IdUser))
                    context.AddFailure("username", "Username exist!");
                if (user.Any(x => x.Phone == data.Phone && x.Id != student.IdUser))
                    context.AddFailure("phone", "Phone number registered!");
            });
        }
    }
}
