using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.Base;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;
using static SealabAPI.Helpers.FileHelper;

namespace SealabAPI.DataAccess.Entities
{
    public class PreliminaryAssignmentAnswer : BaseEntity
    {
        public Guid IdUser { get; set; }
        public Guid IdQuestion { get; set; }
        public string Answer { get; set; }
        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }
        [ForeignKey(nameof(IdQuestion))]
        public PreliminaryAssignmentQuestion Question { get; set; }
    }
}
