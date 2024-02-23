using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using SealabAPI.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SealabAPI.Controllers
{
    [Authorize(Roles = "Assistant,Student")]
    [Route("[controller]")]
    [ApiController]
    public class GFormSurveyController : BaseController<
        CreateGFormSurveyRequest,
        UpdateGFormSurveyRequest,
        DetailGFormSurveyResponse,
        GFormSurvey>
    {
        private readonly ILogger<GFormSurveyController> _logger;
        private readonly IGFormSurveyService _service;
        public GFormSurveyController(ILogger<GFormSurveyController> logger, IGFormSurveyService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public override async Task<ActionResult> Create(CreateGFormSurveyRequest model)
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

        [HttpGet("verify/{id}")]
        public virtual async Task<ActionResult> Verify(Guid id)
        {
            try
            {
                bool isValid = await _service.Verify(id);

                return new SuccessApiResponse(string.Format(MessageConstant.Success), new { isValid });
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdateGFormSurveyRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
