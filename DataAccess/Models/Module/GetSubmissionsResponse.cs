using AutoMapper;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class GetSubmissionsResponse : BaseModel
    {
        public string Nim { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int Day { get; set; }
        public int Shift { get; set; }
        public string ModuleInfo { get; set; }
        public int PRTScore { get; set; }
        public List<DetailPreTestAnswerResponse> PRTDetail { get; set; }
        public string PAFilePath { get; set; }
        public string JFilePath { get; set; }
        public DateTime? PASubmitTime { get; set; }
        public DateTime? JSubmitTime { get; set; }
        public Feedback Feedback { get; set; }
        public GetSubmissionsResponse()
        {
            IncludeProperty(new string[] { "Student", "Student.User", "Module" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            if (entity is JournalAnswer j)
            {
                JFilePath = j.FilePath;
                JSubmitTime = j.SubmitTime;
                Feedback = new Feedback{
                    Assistant = j.AssistantFeedback,
                    Session = j.SessionFeedback,
                    Laboratory = j.LaboratoryFeedback
                };
            }
            else if (entity is PreliminaryAssignmentAnswer pa)
            {
                PAFilePath = pa.FilePath;
                PASubmitTime = pa.SubmitTime;
            }
            else if (entity is Module module)
                ModuleInfo = $"Module {module.SeelabsId}: {module.Name}";
            else
                base.MapToModel(entity);
        }
    }
}
