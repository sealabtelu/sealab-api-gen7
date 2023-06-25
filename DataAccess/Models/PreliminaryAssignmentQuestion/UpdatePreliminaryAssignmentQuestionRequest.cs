using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdatePreliminaryAssignmentQuestionRequest : BaseModel
    {
        public Guid Id { get; set; }
        public int Module { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public IFormFile File { get; set; }
    }
}
