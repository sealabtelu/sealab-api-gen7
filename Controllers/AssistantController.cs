using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using SealabAPI.Helpers;

namespace SealabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistantController : BaseController<
        CreateAssistantRequest, 
        UpdateAssistantRequest,
        DeleteAssistantRequest, 
        DetailAssistantResponse,
        ListAssistantResponse, 
        Assistant>
    {
        private readonly ILogger<AssistantController> _logger;
        private readonly IAssistantService _service;
        public AssistantController(ILogger<AssistantController> logger, IAssistantService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        public async override Task<ActionResult> Create(CreateAssistantRequest model)
        {
            try
            {
                Assistant result = await _service.Create(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpPost("excel/insert")]
        public async Task<dynamic> ExcelInsert(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                List<CreateAssistantRequest> excel = FileHelper.GetExcelData<CreateAssistantRequest>(file, cancellationToken);
                var result = await _service.BulkInsert(excel);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
