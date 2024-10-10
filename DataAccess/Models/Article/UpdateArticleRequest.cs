using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateArticleRequest : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdAssistant { get; set; }
        public Guid IdCategory { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IFormFile Thumbnail { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            Article article = base.MapToEntity<TEntity>() as Article;
            article.File = Thumbnail;
            article.ThumbnailUrl = $"{article.Id}";
            return article as TEntity;
        }
    }
}
