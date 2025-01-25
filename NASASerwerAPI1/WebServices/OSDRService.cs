using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NASASerwerAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NASASerwerAPI.WebServices
{
    public class OSDRService
    {
        private IConfiguration _configuration { get; set; }
        private IMapper _mapper { get; set; }
        private HttpClient _httpClient { get; set; }

        private Dictionary<String, String> _vehicleNametoUrl { get; set; }

        private Dictionary<String, String> _missionNametoUrl { get; set; }

        private List<MissionModel> _missionModels { get; set; }

        private string _url  { get; set;}
        private string _apikey { get; set; }

        public OSDRService(IConfiguration configuration, IMapper mapper, HttpClient httpClient, Dictionary<String, String> vehicleNametoUrl, Dictionary<String, String> missionNametoUrl, List<MissionModel> missionModels)
        {
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClient;
            _vehicleNametoUrl = vehicleNametoUrl;
            _missionNametoUrl = missionNametoUrl;
            _missionModels = missionModels;
            _url = _configuration["_url:OSDR"];
            _apikey = _configuration["Tokens:API-KEY"];
        }

        public async Task<Dictionary<string, string>> GetAllVehicles() {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-key-api",_apikey);
            HttpResponseMessage response = await _httpClient.GetAsync(_url);
            String responseStr = await response.Content.ReadAsStringAsync();
            JObject responseJson = JObject.Parse(responseStr);

            foreach (JToken vehicleURLStr in responseJson.GetValue("data").Children()) {
                VehicleURL vehicleURL = JsonConvert.DeserializeObject<VehicleURL>(vehicleURLStr.ToString());
                _vehicleNametoUrl.TryAdd(vehicleURL.Vehicle.Split('/').Last(), vehicleURL.Vehicle);
            }
            return _vehicleNametoUrl;
        }
        public async Task<Dictionary<string, string>> GetVehicle(string vehicleURL) {
            _missionNametoUrl.Clear();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-key-api", _apikey);
            HttpResponseMessage response = await _httpClient.GetAsync(vehicleURL);
            String responseStr = await response.Content.ReadAsStringAsync();
            JObject responseJson = JObject.Parse(responseStr);

            foreach (JToken missionUrlStr in responseJson.GetValue("parents")["mission"].Children())
            {
                MissionURL missionURL = JsonConvert.DeserializeObject<MissionURL>(missionUrlStr.ToString());
                _missionNametoUrl.TryAdd(missionURL.Mission.Split('/').Last(), missionURL.Mission);
            }
            return _missionNametoUrl;
        }
        public async Task<List<MissionModelUser>> GetMissionDetails(string missionURL) {
            _missionModels.Clear();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-key-api", _apikey);
            HttpResponseMessage response = await _httpClient.GetAsync(missionURL);
            String responseStr = await response.Content.ReadAsStringAsync();
            JObject responseJson = JObject.Parse(responseStr);
            foreach (JToken mission in responseJson.GetValue("people").Children()) { 
                MissionModel person = JsonConvert.DeserializeObject<MissionModel>(mission.ToString());
                _missionModels.Add(person);
            }

            return _mapper.Map<List<MissionModelUser>>(_missionModels);
        }


    }
}
