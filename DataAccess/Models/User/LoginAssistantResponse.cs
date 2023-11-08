using SealabAPI.Base;

namespace SealabAPI.DataAccess.Models
{
    public class LoginAssistantResponse : BaseModel
    {
        public Guid IdUser { get; set; }
        public Guid IdAssistant { get; set; }
        public string Nim { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string AppToken { get; set; }
        public dynamic Seelabs { get; set; }
    }
}
