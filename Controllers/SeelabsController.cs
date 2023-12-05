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
    [Authorize(Roles = "Assistant")]
    [ApiController]
    public class SeelabsController : ControllerBase
    {
        private readonly ILogger<SeelabsController> _logger;
        private readonly SeelabsPracticumService _practicum;
        private readonly SeelabsProctorService _proctor;
        public SeelabsController(ILogger<SeelabsController> logger, SeelabsPracticumService practicum, SeelabsProctorService proctor)
        {
            _logger = logger;
            _practicum = practicum;
            _proctor = proctor;
        }
        [HttpGet("schedule")]
        public async Task<ActionResult<List<SeelabsScheduleResponse>>> Schedule()
        {
            try
            {
                var data = await _practicum.Schedule();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("input-overview")]
        public async Task<ActionResult<List<SeelabsScheduleResponse>>> InputOverview([FromQuery] ScoreListRequest model)
        {
            try
            {
                var data = await _practicum.InputOverview(new SeelabsScoreListRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("proctor/schedule")]
        public async Task<ActionResult<List<SeelabsProctorResponse>>> ProctorSchedule()
        {
            try
            {
                var data = await _proctor.Schedule();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("bap")]
        public async Task<ActionResult<List<SeelabsBAPResponse>>> BAP([FromQuery] BAPRequest model)
        {
            try
            {
                var data = await _practicum.BAP(new SeelabsBAPRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("score")]
        public async Task<ActionResult<List<SeelabsListGroupResponse>>> ScoreList([FromQuery] ScoreListRequest model)
        {
            try
            {
                var data = await _practicum.ScoreList(new SeelabsScoreListRequest(model));
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
                var data = await _practicum.ScoreDelete(new SeelabsScoreDeleteRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPut("score")]
        public async Task<ActionResult> ScoreUpdate(ScoreUpdateRequest model)
        {
            try
            {
                var data = await _practicum.ScoreUpdate(model);
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
                var data = await _practicum.ScoreInput(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [AllowAnonymous]
        [Authorize(Roles = "Student")]
        [HttpGet("score/student")]
        public async Task<ActionResult<List<SeelabsScoreStudentResponse>>> ScoreStudent()
        {
            try
            {
                var data = await _practicum.ScoreStudent();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("score/detail")]
        public async Task<ActionResult<SeelabsScoreDetailResponse>> ScoreDetail(ScoreResultRequest model)
        {
            try
            {
                var data = await _practicum.ScoreDetail(new SeelabsScoreDetailRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("score/result")]
        public async Task<ActionResult<SeelabsScoreResultResponse>> ScoreResult(ScoreResultRequest model)
        {
            try
            {
                var data = await _practicum.ScoreResult(new SeelabsScoreResultRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("group/detail")]
        public async Task<ActionResult<List<SeelabsDetailGroupResponse>>> DetailGroup(DetailGroupRequest model)
        {
            try
            {
                var data = await _practicum.GroupDetail(new SeelabsDetailGroupRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("group/list")]
        public async Task<ActionResult<List<SeelabsListGroupResponse>>> ListGroup(ListGroupRequest model)
        {
            try
            {
                var data = await _practicum.GroupList(new SeelabsListGroupRequest(model));
                return new SuccessApiResponse(string.Format(MessageConstant.Success), data);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
