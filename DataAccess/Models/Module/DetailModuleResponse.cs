using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailModuleResponse : BaseModel
    {
        public Guid Id { get; set; }
        public int SeelabsId { get; set; }
        public string Name { get; set; }
        public bool IsPAOpen { get; set; }
        public bool IsPRTOpen { get; set; }
    }
}
