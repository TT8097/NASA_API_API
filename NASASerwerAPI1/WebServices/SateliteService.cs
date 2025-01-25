using NASASerwerAPI.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NASASerwerAPI.WebServices
{
    public class SateliteService
    {
        private IConfiguration _config;
        private Dictionary<String, SatelliteModel> _dictSatelites { get; set; }
        private HttpClient _httpClient { get; set; }
        private string _url { get; set; }
        private string _apikey { get; set; }
        public SateliteService(IConfiguration configuration, Dictionary<String, SatelliteModel> dictSatelite, HttpClient httpClient)
        {
            _config = configuration;
            _dictSatelites = dictSatelite;
            _httpClient = httpClient;
            _url = _config["_url:Satellite"];
            _apikey = _config["Tokens:API-KEY"];
        }
        public async Task<Dictionary<String, SatelliteModel>> GetSatelliteInfo()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apikey);
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(_url);
            string odp = await httpResponse.Content.ReadAsStringAsync();


            JObject RawFile = JObject.Parse(odp);
            foreach (JToken jobject in RawFile.GetValue("member").Children()) {
                SatelliteModel satelliteModel = JsonConvert.DeserializeObject<SatelliteModel>(jobject.ToString());

                _dictSatelites.TryAdd(satelliteModel.Name,satelliteModel);

            }
            return _dictSatelites;
        }

        public async Task<SatelliteModel> GetInfoAboutSingleSatellite(String satelliteName)
        {
            SatelliteModel satellite = _dictSatelites[satelliteName];
            return satellite;
        }
    }
}
