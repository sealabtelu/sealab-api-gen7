using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;
using System.Reflection;

namespace SealabAPI.DataAccess.Entities
{
    public class PreTestAnswer : BaseEntity
    {
        public Guid IdOption { get; set; }
        public Guid IdStudent { get; set; }
        [ForeignKey(nameof(IdOption))]
        public PreTestOption Option { get; set; }
        [ForeignKey(nameof(IdStudent))]
        public Student Student { get; set; }
        public Module GetModule()
        {
            return Option.Question.Module;
        }
        public PreTestQuestion GetQuestion()
        {
            return Option.Question;
        }
    }
}
