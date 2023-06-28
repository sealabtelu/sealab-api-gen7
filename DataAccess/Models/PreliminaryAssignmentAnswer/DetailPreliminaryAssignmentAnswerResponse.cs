using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailPreliminaryAssignmentAnswerResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdQuestion { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DetailPreliminaryAssignmentAnswerResponse()
        {
            IncludeProperty(new string[] { "User", "Question" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            if (entity is PreliminaryAssignmentAnswer model)
            {
                Name = model.User.Name;
                Question = model.Question.Text;
            }
        }
    }
}
