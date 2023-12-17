using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailGFormSurveyResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string IdUser { get; set; }
        public dynamic Response { get; set; }
    }
}
