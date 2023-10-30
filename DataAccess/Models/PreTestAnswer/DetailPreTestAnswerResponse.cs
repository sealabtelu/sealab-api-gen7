using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailPreTestAnswerResponse : BaseModel
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public string ModuleInfo { get; set; }
        public int PRTScore { get; set; }
        public List<PreTestAnswerDetail> PRTDetail { get; set; }
        public DetailStudentResponse StudentInfo { get; set; } = new();
        public override void MapToModel<TEntity>(TEntity entity)
        {
            if (entity is Module module)
                ModuleInfo = $"Module {module.SeelabsId}: {module.Name}";
            else
                base.MapToModel(entity);
        }
    }
}
