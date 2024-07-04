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
    public class PostController : BaseController<
        CreatePostRequest,
        UpdatePostRequest,
        DetailPostResponse,
        Post>
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _service;
        public PostController(ILogger<PostController> logger, IPostService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }
        [AllowAnonymous]
        public override Task<ActionResult<DetailPostResponse>> GetById(Guid id)
        {
            return base.GetById(id);
        }
        [AllowAnonymous]
        public override ActionResult<List<DetailPostResponse>> GetList()
        {
            return base.GetList();
        }
        public override Task<ActionResult> Create([FromForm] CreatePostRequest model)
        {
            return base.Create(model);
        }

    }
}
