using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailJournalAnswerResponse : BaseModel
    {
        public Guid Id { get; set; }
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public string ModuleInfo { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmitTime { get; set; }
        public string AssistantFeedback { get; set; }
        public string SessionFeedback { get; set; }
        public string LaboratoryFeedback { get; set; }
        public DetailStudentResponse StudentInfo { get; set; } = new();
        public DetailJournalAnswerResponse()
        {
            IncludeProperty(new string[] { "Student", "Student.User", "Module" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            JournalAnswer answer = entity as JournalAnswer;
            base.MapToModel(answer);
            StudentInfo.MapToModel(answer.Student);
            ModuleInfo = $"Module {answer.Module.SeelabsId}: {answer.Module.Name}";
        }
    }
}
