using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SealabAPI.DataAccess.Entities
{
    public class Student : BaseEntity
    {
        public Guid IdUser { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public string Day { get; set; }
        public int Shift { get; set; }
        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }
    }
}
