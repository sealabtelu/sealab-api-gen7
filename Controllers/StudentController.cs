using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using SealabAPI.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : BaseController<
        CreateStudentRequest,
        UpdateStudentRequest,
        DetailStudentResponse,
        Student>
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _service;
        public StudentController(ILogger<StudentController> logger, IStudentService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public override ActionResult<List<DetailStudentResponse>> GetList()
        {
            try
            {
                List<DetailStudentResponse> models = _baseService.GetAll<DetailStudentResponse>().OrderBy(x => x.Day).ThenBy(x => x.Shift).ThenBy(x => x.Group).ToList();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        public async override Task<ActionResult> Create(CreateStudentRequest model)
        {
            try
            {
                Student result = await _service.Create(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [AllowAnonymous]
        [Authorize]
        public async override Task<ActionResult> Update(UpdateStudentRequest model)
        {
            try
            {
                var result = await _service.Update(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("excel/insert")]
        public async Task<ActionResult> ExcelInsert(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                List<CreateStudentRequest> excel = FileHelper.GetExcelData<CreateStudentRequest>(file, cancellationToken);
                await _service.BulkInsert(excel);
                return new SuccessApiResponse(string.Format(MessageConstant.Success));
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
