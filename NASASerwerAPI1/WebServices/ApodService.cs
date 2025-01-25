using AutoMapper;
using NASASerwerAPI.Models;
using Newtonsoft.Json;

namespace NASASerwerAPI.WebServices
{
    public class ApodService
    {
        private IConfiguration _configuration { get; set; }
        private IMapper _mapper {  get; set; }
        private HttpClient _httpClient { get; set; }
         
        private string _url { get; set; }

        private string _apikey { get; set; }

        public ApodService(IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _httpClient = httpClient;
            _url = _configuration["_url:APOD"];
            _apikey = _configuration["Tokens:API-KEY"];
        }

        public async Task<APODUser> GetApodData() {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apikey);
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(_url);
            string odp = await httpResponse.Content.ReadAsStringAsync();
            APODRaw apod = JsonConvert.DeserializeObject<APODRaw>(odp);
            APODUser apodUser = _mapper.Map<APODRaw, APODUser>(apod);

            return apodUser;
        }
    }
}
