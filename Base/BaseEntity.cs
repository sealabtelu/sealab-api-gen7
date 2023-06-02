using System.ComponentModel.DataAnnotations;

namespace SealabAPI.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
