using Microsoft.AspNetCore.Mvc;
using NASASerwerAPI.WebServices;

namespace NASASerwerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class system : Controller
    {
        private IConfiguration _configuration { get; set; }
        private ApodService _apod { get; set; }

        public system(IConfiguration configuration, ApodService apod)
        {
            this._configuration = configuration;
            this._apod = apod;
        }

        [HttpGet]
        public bool IsNASAServiceAvaliable() {
            var result = _apod.GetApodData();

            if (result.Result.Explanation != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public string GetSysInfo() {
            return  "Autorzy: " + _configuration.GetValue<string>($"SystemInfo:Authors")
                + $"{Environment.NewLine}API-Version: " + _configuration.GetValue<string>("SystemInfo:API-Version")
                + $"{Environment.NewLine}Data: " + _configuration.GetValue<string>("SystemInfo:Date");
        }
    }
}
