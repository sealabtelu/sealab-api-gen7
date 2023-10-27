using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DetailModuleResponse : BaseModel
    {
        public Guid Id { get; set; }
        public int SeelabsId { get; set; }
        public string Name { get; set; }
        public bool IsPAOpen { get; set; }
        public bool IsPRTOpen { get; set; }
        public bool IsJOpen { get; set; }
        public int PACount { get; set; }
        public int PRTCount { get; set; }
        public DetailModuleResponse()
        {
            IncludeProperty(new string[] { "PAQuestions", "PRTQuestions" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            if (entity is Module module)
            {
                base.MapToModel(module);
                PACount = module.PAQuestions.Count;
                PRTCount = module.PRTQuestions.Count;
            }
        }
    }
}
