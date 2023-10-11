using FluentValidation;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreateModuleRequest : BaseModel
    {
        public int SeelabsId { get; set; }
        public string Name { get; set; }
    }
    public class CreateModuleRequestRequestValidator : AbstractModelValidator<CreateModuleRequest>
    {
        private readonly AppDbContext _dbContext;
        public CreateModuleRequestRequestValidator(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("No module name provided");
        }
    }

}
