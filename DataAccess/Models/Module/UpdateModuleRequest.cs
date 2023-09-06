using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateModuleRequest : BaseModel
    {
        public Guid Id { get; set; }
        public int SeelabsId { get; set; }
        public string Name { get; set; }
    }
}
