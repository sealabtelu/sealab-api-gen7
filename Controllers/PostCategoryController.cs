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
    public class PostCategoryController : BaseController<
        CreatePostCategoryRequest,
        UpdatePostCategoryRequest,
        DetailPostCategoryResponse,
        PostCategory>
    {
        private readonly ILogger<PostCategoryController> _logger;
        private readonly IPostCategoryService _service;
        public PostCategoryController(ILogger<PostCategoryController> logger, IPostCategoryService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
    }
}
