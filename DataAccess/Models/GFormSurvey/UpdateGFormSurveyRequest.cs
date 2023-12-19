using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateGFormSurveyRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
