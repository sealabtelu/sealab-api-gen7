using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using SealabAPI.Helpers;
using System.Net.Mime;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JournalAnswerController : BaseController<
        CreateJournalAnswerRequest,
        UpdateJournalAnswerRequest,
        DetailJournalAnswerResponse,
        JournalAnswer>
    {
        private readonly ILogger<JournalAnswerController> _logger;
        private readonly IJournalAnswerService _service;
        public JournalAnswerController(ILogger<JournalAnswerController> logger, IJournalAnswerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public override Task<ActionResult> Create([FromForm] CreateJournalAnswerRequest model)
        {
            return base.Create(model);
        }
        [HttpGet("download-zip/{module}")]
        public ActionResult DownloadZip(string module)
        {
            FileStream fs = FileHelper.DownloadFolderZip(new string[] { "Journal", $"J{module}", "Submission" });
            return File(fileStream: fs, contentType: MediaTypeNames.Application.Zip, fileDownloadName: "Submission.zip");
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdateJournalAnswerRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
