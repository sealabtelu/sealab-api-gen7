using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
        public DetailStudentResponse StudentInfo { get; set; } = new();
        public DetailGFormSurveyResponse()
        {
            IncludeProperty(new string[] { "User" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            if (entity is GFormSurvey gForm)
            {
                base.MapToModel(gForm);
                StudentInfo.MapToModel(gForm.User.Student);
                Response = JsonConvert.DeserializeObject<ExpandoObject>(gForm.Response);
            }
        }
    }
}
