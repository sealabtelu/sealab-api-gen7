using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateAssistantRequest : BaseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
    }
    public class UpdateAssistantRequestValidator : AbstractModelValidator<UpdateAssistantRequest>
    {
        public UpdateAssistantRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                var user = dbContext.Set<User>().AsNoTracking();
                Assistant assistant = dbContext.Set<Assistant>().Include(x => x.User).Where(s => s.Id == data.Id).AsNoTracking().FirstOrDefault();
                if (user.Any(x => x.Username == data.Username && x.Id != assistant.IdUser))
                    context.AddFailure("username", "Username exist!");
                if (user.Any(x => x.Phone == data.Phone && x.Id != assistant.IdUser))
                    context.AddFailure("phone", "Phone number registered!");
            });
        }
    }
}
