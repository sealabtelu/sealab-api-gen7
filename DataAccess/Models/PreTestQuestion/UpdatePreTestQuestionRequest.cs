using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdatePreTestQuestionRequest : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdModule { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        // public IFormFile File { get; set; }
        public List<PTOptionDetail> Options { get; set; }

        public override TEntity MapToEntity<TEntity>()
        {
            TEntity question = base.MapToEntity<TEntity>();
            foreach (var option in Options)
            {
                PreTestOption entity = option.MapToEntity<PreTestOption>();
                entity.IdQuestion = Id;
                (question as PreTestQuestion).PTOptions.Add(entity);
            }
            return question;
        }
    }
}
