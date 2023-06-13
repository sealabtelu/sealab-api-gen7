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
        public string Email { get => _email; set => _email = _email == null ? value + "@student.telkomuniversity.ac.id" : value; }
        public string Name { get => _name; set => _name = value?.ToTitleCase(); }
        public string Username { get => _username; set => _username = value?.ToLower(); }
        public string Password { get => _password; set => _password = _password == null ? value?.HashPassword() : value; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string AppToken { get; set; }
    }
}
