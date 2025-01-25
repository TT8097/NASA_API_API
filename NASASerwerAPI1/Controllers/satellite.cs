using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NASASerwerAPI.Models;
using NASASerwerAPI.WebServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace NASASerwerAPI.Controllers
{
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class satellite : Controller
    {
        private IConfiguration _configuration { get; set; }

        private Dictionary<String, SatelliteModel> _dictSatelites { get; set; }

        private SateliteService _sateliteService { get; set; }

        public satellite(IConfiguration configuration, Dictionary<string, SatelliteModel> dictSatelites, SateliteService sateliteService)
        {
            _configuration = configuration;
            this._dictSatelites = dictSatelites;
            this._sateliteService = sateliteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSatelliteNames() 
        {
            if (IsTokenRight()) 
            {
                return Ok(await _sateliteService.GetSatelliteInfo());
            }

            return Unauthorized("Niepoprawny token");
        }

        [HttpPost]
        public async Task<IActionResult> GetInfoAboutSatellite([FromBody] SatelliteRequest request)
        {
            if (IsTokenRight())
            {
                return Ok(await _sateliteService.GetInfoAboutSingleSatellite(request.SatelliteName));
            }

            return Unauthorized("Niepoprawny token");
        }
       
        private bool IsTokenRight() {

            if (Request.Headers["Authentication"].ToString() != null && Request.Headers["Authentication"].Equals(_configuration["Tokens:Client-Token"])) { 
                
                return true;
       
            }
            
            return false;
        
        }

    }
}
