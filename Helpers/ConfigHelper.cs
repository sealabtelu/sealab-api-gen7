using System.Globalization;
using System.Web;
// using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SealabAPI.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string GetConfig(string key, string defaultValue = "Not Found") =>
            Environment.GetEnvironmentVariable(key) ??
            _configuration[key] ??
            _configuration.GetConnectionString(key) ??
            defaultValue;
    }
}
