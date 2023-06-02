using SealabAPI.Base;

namespace SealabAPI.DataAccess.Models
{
    public class LoginRequest : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
