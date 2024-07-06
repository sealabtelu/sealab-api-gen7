using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Article> Posts { get; set; } = new HashSet<Article>();
    }
}
