using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreTestAnswerRequest : BaseModel
    {
        public Guid IdStudent { get; set; }
        public List<Guid> IdAnswers { get; set; }
        public override List<TEntity> MaptoListEntity<TEntity>()
        {
            List<TEntity> answers = new();
            foreach (Guid answer in IdAnswers)
            {
                answers.Add(new PreTestAnswer
                {
                    IdOption = answer,
                    IdStudent = IdStudent
                } as TEntity);
            }
            return answers;
        }
    }
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }
    public class CreatePreTestAnswerRequestValidator : AbstractModelValidator<CreatePreTestAnswerRequest>
    {
        public CreatePreTestAnswerRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.IdStudent).NotNull().NotEmpty().WithMessage("ID is Required");
            RuleFor(x => x.IdAnswers).Must(x => x != null).WithMessage("ID is Required");
            RuleFor(x => x).Custom((data, context) =>
            {
                if (data.IdAnswers != null)
                {
                    var answers = dbContext.Set<PreTestAnswer>()
                                .Include(x => x.Option)
                                .ThenInclude(x => x.Question)
                                .ThenInclude(x => x.Module).AsNoTracking();
                    Guid idOption = data.IdAnswers.FirstOrDefault();
                    Guid IdModule = dbContext.Set<PreTestOption>().Include(x => x.Question).ThenInclude(x => x.Module).FirstOrDefault(x => x.Id == idOption).Question.Module.Id;
                    if (answers.Any(x => x.Option.Question.Module.Id == IdModule && x.IdStudent == data.IdStudent))
                        context.AddFailure("module", "Assignment already submitted!");
                }
            });
        }
    }
}
