using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdatePreliminaryAssignmentQuestionRequest : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdModule { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string AnswerKey { get; set; }
        public IFormFile File { get; set; }
    }
}
