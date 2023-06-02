using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SealabAPI.DataAccess;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace SealabAPI.Base
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Create<TModel>(TModel model) where TModel : BaseModel;

        Task<TEntity> Update<TModel>(TModel model) where TModel : BaseModel;

        Task<int> Delete(Guid id);

        Task<TModel> Get<TModel>(Expression<Func<TEntity, bool>> expression = null) where TModel : BaseModel, new();

        List<TModel> GetAll<TModel>(Expression<Func<TEntity, bool>> expression = null) where TModel : BaseModel, new();
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity: BaseEntity
    {
        protected readonly AppDbContext _appDbContext;
        public BaseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task<TEntity> Create<TModel>(TModel model) where TModel : BaseModel
        {
            TEntity entity = model.MapToEntity<TEntity>();
            await _appDbContext.Set<TEntity>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update<TModel>(TModel model) where TModel : BaseModel
        {
            TEntity entity = model.MapToEntity<TEntity>();
            TEntity data = await _appDbContext.Set<TEntity>().FindAsync(entity.Id);
            if (data != null)
            {
                foreach (PropertyInfo propertyInfo in data.GetType().GetProperties())
                {
                    var source = entity.GetType().GetProperty(propertyInfo.Name).GetValue(entity);
                    var target = propertyInfo.GetValue(data);
                    if (source != null && !source.Equals(target))
                        propertyInfo.SetValue(data, source);
                    else if (source?.ToString() == "none")
                        propertyInfo.SetValue(data, null);
                }
                _appDbContext.Set<TEntity>().Update(data);
                await _appDbContext.SaveChangesAsync();
            }
            return data;
        }

        public virtual async Task<int> Delete(Guid id)
        {
            TEntity data = await _appDbContext.Set<TEntity>().FindAsync(id);
            if (data != null)
                _appDbContext.Set<TEntity>().Remove(data);

            return await _appDbContext.SaveChangesAsync();
        }

        public virtual async Task<TModel> Get<TModel>(Expression<Func<TEntity, bool>> expression = null) where TModel : BaseModel, new()
        {
            TModel model = new();
            TEntity data;
            if (expression == null)
                data = await _appDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync();
            else
                data = await _appDbContext.Set<TEntity>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
            model.MapToModel(data);
            return model;
        }

        public virtual List<TModel> GetAll<TModel>(Expression<Func<TEntity, bool>> expression = null) where TModel : BaseModel, new()
        {
            List<TModel> models = new();
            List<TEntity> entities;
            if (expression == null)
                entities = _appDbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                entities = _appDbContext.Set<TEntity>().Where(expression).AsNoTracking().ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    TModel model = new();
                    model.MapToModel(entity);
                    models.Add(model);
                }
            return models;
        }
    }
}
