using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class Module : BaseEntity
    {
        public int SeelabsId { get; set; }
        public string Name { get; set; }
        public bool IsPAOpen { get; set; } = false;
        public bool IsPRTOpen { get; set; } = false;
        public bool IsJOpen { get; set; } = false;
        public ICollection<PreliminaryAssignmentQuestion> PAQuestions { get; set; } = new HashSet<PreliminaryAssignmentQuestion>();
        public ICollection<PreliminaryAssignmentAnswer> PAAnswers { get; set; } = new HashSet<PreliminaryAssignmentAnswer>();
        public ICollection<PreTestQuestion> PreTests { get; set; } = new HashSet<PreTestQuestion>();
        public ICollection<JournalAnswer> JAnswers { get; set; } = new HashSet<JournalAnswer>();
    }
}
