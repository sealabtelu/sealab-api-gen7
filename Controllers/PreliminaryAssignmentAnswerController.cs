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
    public class PreliminaryAssignmentAnswerController : BaseController<
        CreatePreliminaryAssignmentAnswerRequest,
        UpdatePreliminaryAssignmentAnswerRequest,
        DeletePreliminaryAssignmentAnswerRequest,
        DetailPreliminaryAssignmentAnswerResponse,
        ListPreliminaryAssignmentAnswerResponse,
        PreliminaryAssignmentAnswer>
    {
        private readonly ILogger<PreliminaryAssignmentAnswerController> _logger;
        private readonly IPreliminaryAssignmentAnswerService _service;
        public PreliminaryAssignmentAnswerController(ILogger<PreliminaryAssignmentAnswerController> logger, IPreliminaryAssignmentAnswerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public override Task<ActionResult> Create([FromForm] CreatePreliminaryAssignmentAnswerRequest model)
        {
            return base.Create(model);
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdatePreliminaryAssignmentAnswerRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
