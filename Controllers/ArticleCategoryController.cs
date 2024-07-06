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
    public class ArticleCategoryController : BaseController<
        CreateArticleCategoryRequest,
        UpdateArticleCategoryRequest,
        DetailArticleCategoryResponse,
        ArticleCategory>
    {
        private readonly ILogger<ArticleCategoryController> _logger;
        private readonly IArticleCategoryService _service;
        public ArticleCategoryController(ILogger<ArticleCategoryController> logger, IArticleCategoryService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
    }
}
