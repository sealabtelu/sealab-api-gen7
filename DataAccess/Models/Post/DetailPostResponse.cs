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
        public string ThumbnailUrl { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ReadTime { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DetailPostResponse()
        {
            IncludeProperty(new string[] { "Category", "Assistant", "Assistant.User" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            if (entity is Post post)
            {
                IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, DetailPostResponse>()
                    .ForMember(dest => dest.Category, opt => opt.Ignore()))
                    .CreateMapper();
                mapper.Map(post, this);
                string[] words = Content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Author = post.Assistant.User.Name;
                Category = post.Category.Name;
                ReadTime = $"{Math.Ceiling((double)words.Length / 250)} mins";
            }
        }
    }
}
