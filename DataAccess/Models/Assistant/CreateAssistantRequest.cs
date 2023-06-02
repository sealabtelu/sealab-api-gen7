using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class CreateAssistantRequest : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
    }
}
