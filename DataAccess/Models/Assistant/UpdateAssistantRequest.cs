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
                if (user.Any(x => x.Username == data.Username))
                    context.AddFailure("username", "Username exist!");
                if (user.Any(x => x.Nim == data.Phone))
                    context.AddFailure("phone", "Phone number registered!");
            });
        }
    }
}
