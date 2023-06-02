using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SealabAPI.DataAccess;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Models.Constants;

namespace SealabAPI.Base
{
    public class BaseController<
        ModelCreate,
        ModelUpdate,
        ModelDelete,
        ModelDetail,
        ModelList,
        TEntity> : ControllerBase
        where ModelCreate : BaseModel
        where ModelUpdate : BaseModel
        where ModelDelete : BaseModel
        where ModelDetail : BaseModel, new()
        where ModelList : BaseModel, new()
        where TEntity : BaseEntity
    {
        protected IBaseService<TEntity> _baseService;
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        [HttpPost]
        public virtual async Task<ActionResult> Create(ModelCreate model)
        {
            try
            {
                TEntity result = await _baseService.Create(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update(ModelUpdate model)
        {
            try
            {
                var result = await _baseService.Update(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _baseService.Delete(id);

                return new SuccessApiResponse(string.Format(MessageConstant.Success), id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ModelDetail>> GetById(Guid id)
        {
            try
            {
                ModelDetail model = await _baseService.Get<ModelDetail>(x => x.Id == id);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), model);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("list")]
        public virtual ActionResult<List<ModelList>> GetList()
        {
            try
            {
                List<ModelList> models = _baseService.GetAll<ModelList>();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}

