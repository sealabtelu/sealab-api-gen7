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
    public class JournalAnswerController : BaseController<
        CreateJournalAnswerRequest,
        UpdateJournalAnswerRequest,
        DeleteJournalAnswerRequest,
        DetailJournalAnswerResponse,
        ListJournalAnswerResponse,
        JournalAnswer>
    {
        private readonly ILogger<JournalAnswerController> _logger;
        private readonly IJournalAnswerService _service;
        public JournalAnswerController(ILogger<JournalAnswerController> logger, IJournalAnswerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public override Task<ActionResult> Create([FromForm] CreateJournalAnswerRequest model)
        {
            return base.Create(model);
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdateJournalAnswerRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
