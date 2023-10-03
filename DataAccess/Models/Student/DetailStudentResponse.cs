using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailStudentResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public string Username { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Classroom { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
        public DetailStudentResponse()
        {
            IncludeProperty(new string[] { "User" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            Student assistant = entity as Student;
            IdUser = assistant.User.Id;
            Username = assistant.User.Username;
            Nim = assistant.User.Nim;
            Name = assistant.User.Name;
            Phone = assistant.User.Phone;
            Email = assistant.User.Email;
        }
    }
}
