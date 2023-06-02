using SealabAPI.Base;

namespace SealabAPI.DataAccess.Models
{
    public class LoginStudentResponse : BaseModel
    {
        public Guid IdUser { get; set; }
        public string Nim { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public string Day { get; set; }
        public int Shift { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string AppToken { get; set; }
        public string Seelabs { get; set; }
    }
}
