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

namespace SealabAPI.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Assistant")]
    [ApiController]
    public class VPSController : ControllerBase
    {
        private readonly ILogger<VPSController> _logger;
        public VPSController(ILogger<VPSController> logger)
        {
            _logger = logger;
        }
        [HttpGet("deploy")]
        public ActionResult Deploy()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                for (int i = 0; i < 4; i++) currentDirectory = Directory.GetParent(currentDirectory).FullName;
                _logger.LogInformation(currentDirectory);
                string scriptPath = Path.Combine(currentDirectory, "deploy.sh");
                Process process = new()
                {
                    StartInfo = new()
                    {
                        FileName = "bash",
                        Arguments = scriptPath,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd().Replace("\n", "");
                process.Close();

                return new SuccessApiResponse(string.Format(MessageConstant.Success), output);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
