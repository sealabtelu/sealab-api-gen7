using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsLoginRequest : BaseModel
    {
        public string user_nim { get; set; }
        public string user_pass { get; set; }
        public string login_ass { get; set; }
        public string submit { get; set; } = "";
        public SeelabsLoginRequest(string nim, string pass, string role)
        {
            user_nim = nim;
            user_pass = pass;
            login_ass = role == "Assistant" ? "2" : "1";
        }
    }
}
