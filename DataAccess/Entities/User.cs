using SealabAPI.Base;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class User : BaseEntity
    {
        private string _username;
        private string _password;
        private string _name;
        private string _phone;
        public string Nim { get; set; }
        public string Email { get; set; }
        public string Name { get => _name; set => _name = value?.ToTitleCase(); }
        public string Username
        {
            get => _username;
            set
            {
                if (value != null)
                {
                    _username = value.ToLower();
                    Email = $"{_username}@student.telkomuniversity.ac.id";
                }
            }
        }
        public string Password { get => _password; set => _password = _password == null ? value?.HashPassword() : value; }
        public string Role { get; set; }
        public string Phone { get => _phone; set => _phone = value?.Replace(" ", ""); }
        public string AppToken { get; set; }
        public Student Student { get; set; }
        public Assistant Assistant { get; set; }
        public GFormSurvey Feedback { get; set; }
    }
}
