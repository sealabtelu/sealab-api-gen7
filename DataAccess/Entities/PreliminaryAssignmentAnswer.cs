using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SealabAPI.DataAccess.Entities
{
    public class PreliminaryAssignmentAnswer : BaseEntity
    {
        public Guid IdStudent { get; set; }
        public Guid IdQuestion { get; set; }
        public string Answer { get; set; }
        [ForeignKey(nameof(IdStudent))]
        public Student Student { get; set; }
        [ForeignKey(nameof(IdQuestion))]
        public PreliminaryAssignmentQuestion Question { get; set; }
    }
}
