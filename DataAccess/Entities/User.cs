using SealabAPI.Base;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Entities
{
    public class User : BaseEntity
    {
        private string _username;
        private string _password;
        private string _name;
        private string _email;
        public string Nim { get; set; }
        public string Email { get => _email; set => _email = value + "@student.telkomuniversity.ac.id"; }
        public string Name { get => _name; set => _name = value?.ToTitleCase(); }
        public string Username { get => _username; set => _username = value?.ToLower(); }
        public string Password { get => _password; set => _password = value?.HashPassword(); }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string AppToken { get; set; }
    }
}
