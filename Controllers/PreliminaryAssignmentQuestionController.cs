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
        ListPreliminaryAssignmentQuestionResponse,
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
        public override Task<ActionResult> Update([FromForm]UpdatePreliminaryAssignmentQuestionRequest model)
        {
            return base.Update(model);
        }
    }
}
