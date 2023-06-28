using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreliminaryAssignmentQuestionRequest : BaseModel
    {
        public int Module { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string AnswerKey { get; set; }
        public IFormFile File { get; set; }
    }
}
