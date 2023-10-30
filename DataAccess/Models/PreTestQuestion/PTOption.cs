using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class PRTOptionStudent : BaseModel
    {
        public Guid Id { get; set; }
        public string Option { get; set; }
    }
    public class PRTOption : BaseModel
    {
        public string Option { get; set; }
        public bool IsTrue { get; set; }
    }
    public class PRTOptionDetail : PRTOption
    {
        public Guid Id { get; set; }
    }
}
