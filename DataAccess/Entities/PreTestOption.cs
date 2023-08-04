using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class PreTestOption : BaseEntity
    {
        public Guid IdQuestion { get; set; }
        public string Option { get; set; }
        public bool IsTrue { get; set; }
        [ForeignKey(nameof(IdQuestion))]
        public PreTestQuestion Question { get; set; }
    }
}
