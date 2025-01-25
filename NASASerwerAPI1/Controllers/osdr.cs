using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NASASerwerAPI.Models;
using NASASerwerAPI.WebServices;

namespace NASASerwerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class osdr : Controller
    {
        private OSDRService _OSDRService { get; set; }
        private IMapper _mapper { get; set; }
        private IConfiguration _configuration { get; set; }

        public osdr(OSDRService _OSDRService, IMapper mapper, IConfiguration configuration) { 
            this._OSDRService = _OSDRService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles() {
            if (IsTokenRight()) 
            {
                return Ok(await _OSDRService.GetAllVehicles());
            }

            return Unauthorized("Niepoprawny token");
        }

        [HttpPost]
        public async Task<IActionResult> GetVehicle([FromBody] OsdrRequest request) {
            if (IsTokenRight())
            {
                return Ok(await _OSDRService.GetVehicle(request.Url));
            }

            return Unauthorized("Niepoprawny token");
        }

        [HttpPost]
        public async Task<IActionResult> GetMissionDetails([FromBody] OsdrRequest request) {
            if (IsTokenRight())
            {
                return Ok(await _OSDRService.GetMissionDetails(request.Url));
            }

            return Unauthorized("Niepoprawny token");
        }

        private bool IsTokenRight()
        {

            if (Request.Headers["Authentication"].ToString() != null && Request.Headers["Authentication"].Equals(_configuration["Tokens:Client-Token"]))
            {

                return true;

            }

            return false;

        }
    }

}
