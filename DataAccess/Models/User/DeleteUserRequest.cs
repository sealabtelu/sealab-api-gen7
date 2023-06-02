using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class DeleteUserRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
