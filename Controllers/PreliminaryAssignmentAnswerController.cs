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
    public class PreliminaryAssignmentAnswerController : BaseController<
        CreatePreliminaryAssignmentAnswerRequest,
        UpdatePreliminaryAssignmentAnswerRequest,
        DetailPreliminaryAssignmentAnswerResponse,
        PreliminaryAssignmentAnswer>
    {
        private readonly ILogger<PreliminaryAssignmentAnswerController> _logger;
        private readonly IPreliminaryAssignmentAnswerService _service;
        public PreliminaryAssignmentAnswerController(ILogger<PreliminaryAssignmentAnswerController> logger, IPreliminaryAssignmentAnswerService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        public override Task<ActionResult> Create([FromForm] CreatePreliminaryAssignmentAnswerRequest model)
        {
            return base.Create(model);
        }
        [AllowAnonymous]
        [HttpGet("download-zip/{module}")]
        public ActionResult DownloadZip(string module)
        {
            FileStream fs = FileHelper.DownloadFolderZip(new string[] { "PreliminaryAssignment", $"TP{module}", "Submission" });
            return File(fileStream: fs, contentType: MediaTypeNames.Application.Zip, fileDownloadName: "Submission.zip");
        }
        [NonAction]
        public override Task<ActionResult> Update(UpdatePreliminaryAssignmentAnswerRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
