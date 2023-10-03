using AutoMapper;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class CreatePreliminaryAssignmentAnswerRequest : BaseModel
    {
        public Guid IdStudent { get; set; }
        public Guid IdModule { get; set; }
        public IFormFile File { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(GetType(), typeof(TEntity))).CreateMapper();
            TEntity entity = (TEntity)mapper.Map(this, GetType(), typeof(TEntity));
            return entity;
        }
    }
}
