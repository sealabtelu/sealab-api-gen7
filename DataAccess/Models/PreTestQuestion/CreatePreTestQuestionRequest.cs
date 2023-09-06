using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreTestQuestionRequest : BaseModel
    {
        public Guid IdModule { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public IFormFile File { get; set; }
        public List<PTOption> Options { get; set; }

        public override TEntity MapToEntity<TEntity>()
        {
            TEntity question = base.MapToEntity<TEntity>();
            foreach (var option in Options)
            {
                (question as PreTestQuestion).PTOptions.Add(option.MapToEntity<PreTestOption>());
            }
            return question;
        }
    }
}
