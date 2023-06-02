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
    [Route("api/[controller]")]
    [ApiController]
    public class SeelabsController : ControllerBase
    {
        private readonly ILogger<SeelabsController> _logger;
        private readonly SeelabsService _modelService;
        public SeelabsController(ILogger<SeelabsController> logger, SeelabsService modelService)
        {
            _logger = logger;
            _modelService = modelService;
        }

        [Authorize(Roles = "Assistant")]
        [HttpPost("score/input")]
        public async Task<ActionResult> ScoreInput(ScoreInputRequest model)
        {
            try
            {
                var data = await _modelService.Score(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [Authorize(Roles = "Assistant")]
        [HttpPost("score/list-group")]
        public async Task<ActionResult> ScoreListGroup(ScoreListGroupRequest model)
        {
            try
            {
                var data = await _modelService.Score(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
