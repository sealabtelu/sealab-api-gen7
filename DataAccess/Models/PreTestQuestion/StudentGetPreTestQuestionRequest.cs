using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class StudentGetPreTestQuestionRequest : BaseModel
    {
        public Guid IdModule { get; set; }
        public Guid IdStudent { get; set; }
    }
    public class StudentGetPreTestQuestionRequestValidator : AbstractModelValidator<StudentGetPreTestQuestionRequest>
    {
        public StudentGetPreTestQuestionRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.IdStudent).NotNull().NotEmpty().WithMessage("ID is Required");
            RuleFor(x => x.IdModule).NotNull().NotEmpty().WithMessage("ID is Required");
            RuleFor(x => x).Custom((data, context) =>
            {
                var answers = dbContext.Set<PreTestAnswer>()
                            .Include(x => x.Option)
                            .ThenInclude(x => x.Question)
                            .ThenInclude(x => x.Module).AsNoTracking();
                if (answers.Any(x => x.Option.Question.Module.Id == data.IdModule && x.IdStudent == data.IdStudent))
                    context.AddFailure("module", "Assignment already submitted!");
            });
        }
    }
}
