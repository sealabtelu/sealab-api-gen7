using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class PTOption : BaseModel
    {
        public string Option { get; set; }
        public bool IsTrue { get; set; }
    }
    public class PTOptionId : PTOption
    {
        public Guid Id { get; set; }
    }
}
