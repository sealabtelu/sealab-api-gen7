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
    public class PreliminaryAssignmentQuestionController : BaseController<
        CreatePreliminaryAssignmentQuestionRequest,
        UpdatePreliminaryAssignmentQuestionRequest,
        DeletePreliminaryAssignmentQuestionRequest,
        DetailPreliminaryAssignmentQuestionResponse,
        PreliminaryAssignmentQuestion>
    {
        private readonly ILogger<PreliminaryAssignmentQuestionController> _logger;
        private readonly IPreliminaryAssignmentQuestionService _service;
        public PreliminaryAssignmentQuestionController(ILogger<PreliminaryAssignmentQuestionController> logger, IPreliminaryAssignmentQuestionService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public override Task<ActionResult> Create([FromForm] CreatePreliminaryAssignmentQuestionRequest model)
        {
            return base.Create(model);
        }
        public override Task<ActionResult> Update([FromForm] UpdatePreliminaryAssignmentQuestionRequest model)
        {
            return base.Update(model);
        }
        [HttpGet("module/{idModule}")]
        public virtual ActionResult<List<DetailPreliminaryAssignmentQuestionResponse>> GetByIdModule(Guid idModule)
        {
            try
            {
                List<DetailPreliminaryAssignmentQuestionResponse> model = _baseService.GetAll<DetailPreliminaryAssignmentQuestionResponse>(x => x.IdModule == idModule);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), model);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
