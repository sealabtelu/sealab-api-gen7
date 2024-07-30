using SealabAPI.Base;

namespace SealabAPI.DataAccess.Models
{
    public class SendEmailRequest : BaseModel
    {
        public string Email { get; set; }
    }
    public class VerifyPasswordRequest : SendEmailRequest
    {
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
