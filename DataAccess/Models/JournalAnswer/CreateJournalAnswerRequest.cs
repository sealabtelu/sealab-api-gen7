using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreateJournalAnswerRequest : BaseModel
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public string AssistantFeedback { get; set; }
        public string SessionFeedback { get; set; }
        public string LaboratoryFeedback { get; set; }
        public IFormFile File { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(GetType(), typeof(TEntity))).CreateMapper();
            TEntity entity = (TEntity)mapper.Map(this, GetType(), typeof(TEntity));
            return entity;
        }
    }
    public class CreateJournalAnswerRequestValidator : AbstractModelValidator<CreateJournalAnswerRequest>
    {
        public CreateJournalAnswerRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                var answer = dbContext.Set<JournalAnswer>().AsNoTracking();
                if (answer.Any(x => x.IdModule == data.IdModule && x.IdStudent == data.IdStudent))
                    context.AddFailure("module", "Assignment already submitted!");
            });
        }
    }
}
