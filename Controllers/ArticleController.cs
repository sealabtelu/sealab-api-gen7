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
    [ApiController]
    public class ArticleController : BaseController<
        CreateArticleRequest,
        UpdateArticleRequest,
        DetailArticleResponse,
        Article>
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleService _service;
        public ArticleController(ILogger<ArticleController> logger, IArticleService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [AllowAnonymous]
        public override Task<ActionResult<DetailArticleResponse>> GetById(Guid id)
        {
            return base.GetById(id);
        }
        [AllowAnonymous]
        public override ActionResult<List<DetailArticleResponse>> GetList()
        {
            return base.GetList();
        }
        public override Task<ActionResult> Create([FromForm] CreateArticleRequest model)
        {
            return base.Create(model);
        }
    }
}
