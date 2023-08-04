using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailPreliminaryAssignmentAnswerResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdStudent { get; set; }
        public Guid IdQuestion { get; set; }
        public int Module { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DetailPreliminaryAssignmentAnswerResponse()
        {
            IncludeProperty(new string[] { "Student", "Student.User", "Question" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            PreliminaryAssignmentAnswer answer = entity as PreliminaryAssignmentAnswer;
            Module = answer.Question.Module;
            Question = answer.Question.Question;
        }
    }
}
