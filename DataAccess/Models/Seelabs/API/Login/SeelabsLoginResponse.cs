using System.Net;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsLoginResponse : BaseModel
    {
        public bool Valid { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public SeelabsLoginResponse(string name, Cookie cookie)
        {
            Valid = name != null;
            Name = name;
            Token = cookie?.Value;
            Expires = cookie?.Expires;
        }
    }
}
