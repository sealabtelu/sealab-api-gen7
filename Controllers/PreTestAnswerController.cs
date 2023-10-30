using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PreTestAnswerController : BaseController<
        CreatePreTestAnswerRequest,
        UpdatePreTestAnswerRequest,
        DetailPreTestAnswerResponse,
        PreTestAnswer>
    {
        private readonly ILogger<PreTestAnswerController> _logger;
        private readonly IPreTestAnswerService _service;
        public PreTestAnswerController(ILogger<PreTestAnswerController> logger, IPreTestAnswerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        [StudentRestricted]
        public override Task<ActionResult> Create(CreatePreTestAnswerRequest model)
        {
            return base.Create(model);
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdatePreTestAnswerRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
