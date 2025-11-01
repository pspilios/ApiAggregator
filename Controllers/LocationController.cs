using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly HttpClient http;
        private readonly ILogger<LocationController> logger;
        private readonly IConfiguration config;

        public LocationController(HttpClient htp, ILogger<LocationController> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

        [HttpGet("{cityName}")]
        public async Task<dynamic> GetCoordinates(string cityName)
        {
            try
            {
                var res = await http.GetFromJsonAsync<System.Text.Json.JsonElement[]>(config["ExternalApis:GeocoderApi"]
                                       .Replace("{name}", Uri.EscapeDataString(cityName))
                                       .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:OpenWeatherKey"])));

                if (res.Equals("") || res.Length == 0)
                {
                    throw new Exception($"No result found for city name");
                }

                return res[0].GetProperty("name");
            }
            catch (HttpRequestException e)
            {
                // Network/HTTP error
                Console.WriteLine($"HTTP request failed: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"System threw exception: {e.Message}");
                return null;
            }
        }
    }
}
