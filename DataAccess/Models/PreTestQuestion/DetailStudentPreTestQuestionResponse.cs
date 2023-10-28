using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailStudentPreTestQuestionResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdModule { get; set; }
        public string ModuleInfo { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath { get; set; }
        public List<PTOptionStudent> Options { get; set; } = new();
        public DetailStudentPreTestQuestionResponse()
        {
            IncludeProperty(new string[] { "PTOptions", "Module" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            PreTestQuestion question = entity as PreTestQuestion;
            ModuleInfo = string.Format("Module {0}: {1}", question.Module.SeelabsId, question.Module.Name);
            foreach (var option in question.PTOptions)
            {
                Options.Add(new PTOptionStudent
                {
                    Id = option.Id,
                    Option = option.Option,
                });
            }
            Options = Options.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
