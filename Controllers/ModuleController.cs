using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModuleController : BaseController<
        CreateModuleRequest,
        UpdateModuleRequest,
        DeleteModuleRequest,
        DetailModuleResponse,
        ListModuleResponse,
        Module>
    {
        private readonly ILogger<ModuleController> _logger;
        private readonly IModuleService _service;
        public ModuleController(ILogger<ModuleController> logger, IModuleService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpPost("submission/pa/{idStudent}")]
        public virtual ActionResult<List<ListSubmittedPAResponse>> GetListSubmittedPA(Guid idStudent)
        {
            try
            {
                List<ListSubmittedPAResponse> models = _service.GetListSubmittedPA(idStudent);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        public override ActionResult<List<ListModuleResponse>> GetList()
        {
            try
            {
                List<ListModuleResponse> models = _baseService.GetAll<ListModuleResponse>().OrderBy(x => x.SeelabsId).ToList();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("set-pa-status")]
        public async Task<ActionResult> PAStatus(SetPAStatusRequest model)
        {
            try
            {
                await _service.SetAssignmentStatus(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), "Success");
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("set-prt-status")]
        public async Task<ActionResult> PRTStatus(SetPRTStatusRequest model)
        {
            try
            {
                await _service.SetAssignmentStatus(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), "Success");
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
