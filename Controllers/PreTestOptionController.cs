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
    public class PreTestOptionController : BaseController<
        CreatePreTestOptionRequest,
        UpdatePreTestOptionRequest,
        DeletePreTestOptionRequest,
        DetailPreTestOptionResponse,
        ListPreTestOptionResponse,
        PreTestOption>
    {
        private readonly ILogger<PreTestOptionController> _logger;
        private readonly IPreTestOptionService _service;
        public PreTestOptionController(ILogger<PreTestOptionController> logger, IPreTestOptionService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
    }
}
