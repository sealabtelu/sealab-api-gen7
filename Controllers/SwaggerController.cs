using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Assistant")]
    [ApiController]
    public class SwaggerController : ControllerBase
    {
        private readonly ILogger<SeelabsController> _logger;
        private readonly ISwaggerProvider _swaggerProvider;
        public SwaggerController(ILogger<SeelabsController> logger, ISwaggerProvider swaggerProvider)
        {
            _logger = logger;
            _swaggerProvider = swaggerProvider;
        }
        [HttpGet]
        public ActionResult Schedule()
        {
            try
            {
                var swaggerDoc = _swaggerProvider.GetSwagger("v1");
                swaggerDoc.Components.SecuritySchemes.Clear();
                var swaggerJson = swaggerDoc.SerializeAsJson(OpenApiSpecVersion.OpenApi3_0);

                return Ok(swaggerJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
