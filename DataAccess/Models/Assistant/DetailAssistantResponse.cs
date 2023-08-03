using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class DetailAssistantResponse : BaseModel
    {
        public Guid IdAssistant { get; set; }
        public string Username { get; set; }
        public string Nim { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
        public DetailAssistantResponse()
        {
            IncludeProperty(new string[] { "User" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            Assistant assistant = entity as Assistant;
            IdAssistant = assistant.Id;
            Username = assistant.User.Username;
            Nim = assistant.User.Nim;
            Name = assistant.User.Name;
            Phone = assistant.User.Phone;
            Email = assistant.User.Email;
        }
    }
}
