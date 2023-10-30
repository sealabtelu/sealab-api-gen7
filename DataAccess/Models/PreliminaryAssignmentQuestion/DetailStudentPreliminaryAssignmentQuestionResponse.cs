using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailStudentPreliminaryAssignmentQuestionResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdModule { get; set; }
        public string ModuleInfo { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string FilePath { get; set; }
        public DetailStudentPreliminaryAssignmentQuestionResponse()
        {
            IncludeProperty(new string[] { "Module" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            PreliminaryAssignmentQuestion question = entity as PreliminaryAssignmentQuestion;
            ModuleInfo = $"Module {question.Module.SeelabsId}: {question.Module.Name}";
        }
    }
}
