using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SealabAPI.Base
{
    public abstract class BaseModel
    {
        public virtual TEntity MapToEntity<TEntity>() where TEntity : BaseEntity
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(GetType(), typeof(TEntity))).CreateMapper();
            return (TEntity)mapper.Map(this, GetType(), typeof(TEntity));
        }

        public virtual void MapToModel<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TEntity), GetType())).CreateMapper();
            mapper.Map(entity, this);
        }
    }
}
