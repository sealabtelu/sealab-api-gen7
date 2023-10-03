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
    public class ModuleController : BaseController<
        CreateModuleRequest,
        UpdateModuleRequest,
        DeleteModuleRequest,
        DetailModuleResponse,
        ListModuleResponse,
        Module>
    {
        private readonly ILogger<ModuleController> _logger;
        private readonly IModuleService _service;
        public ModuleController(ILogger<ModuleController> logger, IModuleService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
    }
}