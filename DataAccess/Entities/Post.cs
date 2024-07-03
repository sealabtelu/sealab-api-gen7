using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class Post : BaseEntity
    {
        public Guid IdAssistant { get; set; }
        public Guid IdCategory { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(IdCategory))]
        public PostCategory Category { get; set; }
        [ForeignKey(nameof(IdAssistant))]
        public Assistant Assistant { get; set; }
    }
}
