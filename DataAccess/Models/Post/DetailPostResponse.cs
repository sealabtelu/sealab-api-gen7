using AutoMapper;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailPostResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DetailPostResponse()
        {
            IncludeProperty(new string[] { "Category", "Assistant", "Assistant.User" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            Post post = entity as Post;
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, DetailPostResponse>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()))
                .CreateMapper();
            mapper.Map(post, this);            
            Author = post.Assistant.User.Name;
            Category = post.Category.Name;
        }
    }
}
