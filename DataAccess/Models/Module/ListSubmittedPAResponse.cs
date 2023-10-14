using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class ListSubmittedPAResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsOpen { get; set; }
        public override void MapToModel<TEntity>(TEntity entity) 
        {
            base.MapToModel(entity);
            IsOpen = (entity as Module).IsPAOpen;
        }
    }
}
