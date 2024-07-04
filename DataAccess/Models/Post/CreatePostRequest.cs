using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePostRequest : BaseModel
    {
        public Guid IdAssistant { get; set; }
        public Guid IdCategory { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IFormFile Thumbnail { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            Post post = base.MapToEntity<TEntity>() as Post;
            post.File = Thumbnail;
            post.ThumbnailUrl = $"{post.Id}";
            return post as TEntity;
        }
    }
}
