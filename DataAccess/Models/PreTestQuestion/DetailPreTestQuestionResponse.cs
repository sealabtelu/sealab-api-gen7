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
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath { get; set; }
        public List<PTOptionId> Options { get; set; } = new();
        public DetailPreTestQuestionResponse()
        {
            IncludeProperty(new string[] { "PTOptions" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            PreTestQuestion question = entity as PreTestQuestion;
            foreach (var option in question.PTOptions)
            {
                Options.Add(new PTOptionId
                {
                    Id = option.Id,
                    Option = option.Option,
                    IsTrue = option.IsTrue
                });
            }
        }
    }
}
