using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailPreTestQuestionResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdModule { get; set; }
        public string ModuleInfo { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath { get; set; }
        public List<PRTOptionDetail> Options { get; set; } = new();
        public DetailPreTestQuestionResponse()
        {
            IncludeProperty(new string[] { "PRTOptions", "Module" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            PreTestQuestion question = entity as PreTestQuestion;
            ModuleInfo = $"Module {question.Module.SeelabsId}: {question.Module.Name}";
            foreach (var option in question.PRTOptions)
            {
                Options.Add(new PRTOptionDetail
                {
                    Id = option.Id,
                    Option = option.Option,
                    IsTrue = option.IsTrue
                });
            }
        }
    }
}
