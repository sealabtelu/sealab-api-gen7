using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using SealabAPI.Helpers;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GFormSurveyController : ControllerBase
    {
        private readonly ILogger<GFormSurveyController> _logger;
        private readonly IGFormSurveyService _service;
        public GFormSurveyController(ILogger<GFormSurveyController> logger, IGFormSurveyService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpPost]
        public virtual async Task<ActionResult> Create(CreateGFormSurveyRequest model)
        {
            try
            {
                GFormSurvey result = await _service.Create(new CreateGFormSurvey
                {
                    Response = model.Response,
                    IdUser = Request.ReadToken("nameid")
                });
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
                await _service.Delete(id);

                return new SuccessApiResponse(string.Format(MessageConstant.Success), id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<DetailGFormSurveyResponse>> GetById(Guid id)
        {
            try
            {
                DetailGFormSurveyResponse model = await _service.Get<DetailGFormSurveyResponse>(x => x.Id == id);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), model);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("list")]
        public virtual ActionResult<List<DetailGFormSurveyResponse>> GetList()
        {
            try
            {
                List<DetailGFormSurveyResponse> models = _service.GetAll<DetailGFormSurveyResponse>();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
