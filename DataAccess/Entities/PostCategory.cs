using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class PostCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
