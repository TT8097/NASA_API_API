using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NASASerwerAPI.Models;
using NASASerwerAPI.WebServices;
using Newtonsoft.Json;


namespace NASASerwerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class apod : Controller
    {
        private IConfiguration _configuration { get; set; }
        private IMapper _mapper { get; set; }
        private ApodService _apodService { get; set; }
        public apod(IConfiguration configuration, IMapper mapper, ApodService apodService)
        {
            _configuration = configuration;
            _mapper = mapper;
           _apodService = apodService;
           
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoAndPhotoOfTheDay() {
            if (IsTokenRight())
            {
                return Ok(await _apodService.GetApodData());
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
