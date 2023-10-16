using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreliminaryAssignmentAnswerRequest : BaseModel
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public IFormFile File { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(GetType(), typeof(TEntity))).CreateMapper();
            TEntity entity = (TEntity)mapper.Map(this, GetType(), typeof(TEntity));
            return entity;
        }
    }
    public class CreatePreliminaryAssignmentAnswerRequestValidator : AbstractModelValidator<CreatePreliminaryAssignmentAnswerRequest>
    {
        public CreatePreliminaryAssignmentAnswerRequestValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Custom((data, context) =>
            {
                var answer = dbContext.Set<PreliminaryAssignmentAnswer>().AsNoTracking();
                var module = dbContext.Set<Module>().Where(x => x.Id == data.IdModule).AsNoTracking().FirstOrDefault();
                if (answer.Any(x => x.IdModule == data.IdModule && x.IdStudent == data.IdStudent))
                    context.AddFailure("module", "Assignment already submitted!");
                if (!module.IsPAOpen)
                    context.AddFailure("module", "Time's up!");
            });
        }
    }
}
