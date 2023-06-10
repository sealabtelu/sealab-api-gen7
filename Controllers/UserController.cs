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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<
        CreateUserRequest, UpdateUserRequest,
        DeleteUserRequest, DetailUserResponse,
        ListUserResponse, User>
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService) : base(userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost("login")]
        public virtual async Task<ActionResult> Create(LoginRequest loginRequest)
        {
            try
            {
                object result = await _userService.Login(loginRequest.Username, loginRequest.Password);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
