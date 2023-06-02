using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateAssistantRequest : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
    }
}
