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
    [Authorize(Roles = "Assistant")]
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
        [HttpGet("schedule")]
        public async Task<ActionResult> Schedule()
        {
            try
            {
                var data = await _modelService.Schedule();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("bap")]
        public async Task<ActionResult> BAP([FromQuery] BAPRequest model)
        {
            try
            {
                var data = await _modelService.BAP(model.date);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("score")]
        public async Task<ActionResult> ScoreList([FromQuery] ScoreResultRequest model)
        {
            try
            {
                var data = await _modelService.ScoreResult(model, model.Group != null ? 2 : null);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpDelete("score")]
        public async Task<ActionResult> ScoreDelete(ScoreResultRequest model)
        {
            try
            {
                var data = await _modelService.ScoreResult(model, 3);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("score")]
        public async Task<ActionResult> ScoreInput(ScoreInputRequest model)
        {
            try
            {
                var data = await _modelService.ScoreInput(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("score/detail")]
        public async Task<ActionResult> ScoreDetail(ScoreResultRequest model)
        {
            try
            {
                var data = await _modelService.ScoreResult(model, 1);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("score/list-group")]
        public async Task<ActionResult> ScoreListGroup(ScoreListGroupRequest model)
        {
            try
            {
                var data = await _modelService.ScoreInput(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
