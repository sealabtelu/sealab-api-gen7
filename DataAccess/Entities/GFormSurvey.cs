using SealabAPI.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SealabAPI.DataAccess.Extensions;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class GFormSurvey : BaseEntity
    {
        public Guid IdUser { get; set; }
        public string Response { get; set; }
        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }
    }
}
