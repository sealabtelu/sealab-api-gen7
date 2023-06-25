using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SealabAPI.DataAccess.Entities
{
    public class Assistant : BaseEntity
    {
        public Guid IdUser { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }
    }
}
