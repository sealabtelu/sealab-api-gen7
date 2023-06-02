using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class DeleteAssistantRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
