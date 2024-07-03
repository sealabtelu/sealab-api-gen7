using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdatePostRequest : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdAssistant { get; set; }
        public Guid IdCategory { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
