using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreateGFormSurveyRequest : BaseModel
    {
        public string IdUser { get; set; }
        public string Response { get; set; }
    }
}
