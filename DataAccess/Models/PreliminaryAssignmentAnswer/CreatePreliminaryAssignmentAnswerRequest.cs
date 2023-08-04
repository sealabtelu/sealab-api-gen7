using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreliminaryAssignmentAnswerRequest : BaseModel
    {
        public Guid IdStudent { get; set; }
        public Guid IdQuestion { get; set; }
        public string Answer { get; set; }
    }
}
