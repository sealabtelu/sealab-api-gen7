using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SealabAPI.DataAccess.Entities
{
    public class Student : BaseEntity
    {
        public Guid IdUser { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }
        public ICollection<PreliminaryAssignmentAnswer> PreliminaryAssignments { get; set; } = new HashSet<PreliminaryAssignmentAnswer>();
        public ICollection<PreTestAnswer> PreTestAnswers { get; set; } = new HashSet<PreTestAnswer>();
    }
}
