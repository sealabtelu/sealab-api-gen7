using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models.Constants;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using SealabAPI.Helpers;

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VPSController : ControllerBase
    {
        private readonly ILogger<VPSController> _logger;
        public VPSController(ILogger<VPSController> logger)
        {
            _logger = logger;
        }
        [HttpGet("directory-tree")]
        public ActionResult<List<FileHelper.TreeNode>> GetDirectoryTree(string path)
        {
            try
            {
                var treeDir = FileHelper.GetDirectoryTree(path);

                return new SuccessApiResponse(string.Format(MessageConstant.Success), treeDir);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        [HttpGet("current-time")]
        public ActionResult<string> CurrentTime()
        {
            try
            {
                return new SuccessApiResponse(string.Format(MessageConstant.Success), DateTime.Now);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
