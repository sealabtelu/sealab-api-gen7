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
        [HttpGet]
        public ActionResult Test()
        {

            return new SuccessApiResponse(string.Format(MessageConstant.Success), "success");
        }
        [HttpGet("deploy")]
        public async Task<ActionResult> Deploy()
        {
            try
            {
                string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "deploy-auto.sh");
                _logger.LogInformation(scriptPath);
                Process pullAndMigrate = new()
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
                Process deploy = new()
                {
                    StartInfo = new()
                    {
                        FileName = "dotnet",
                        Arguments = "SealabAPI.dll",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                pullAndMigrate.Start();
                await pullAndMigrate.WaitForExitAsync();
                string output = await pullAndMigrate.StandardOutput.ReadToEndAsync();
                pullAndMigrate.Close();
                deploy.Start();

                return new SuccessApiResponse(string.Format(MessageConstant.Success), output.Replace("\n", ""));
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }
}
