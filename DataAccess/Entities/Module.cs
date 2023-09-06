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
        public ICollection<PreliminaryAssignmentQuestion> PreliminaryAssignments { get; set; } = new HashSet<PreliminaryAssignmentQuestion>();
        public ICollection<PreTestQuestion> PreTests { get; set; } = new HashSet<PreTestQuestion>();
    }
}
