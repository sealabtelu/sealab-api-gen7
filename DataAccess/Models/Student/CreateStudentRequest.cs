using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreateStudentRequest : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
    }
}
