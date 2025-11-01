using ApiAggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static ApiAggregator.Models.OpenWeather;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeatherController : ControllerBase
    {
        private readonly HttpClient http;
        private readonly ILogger<OpenWeatherController> logger;
        private readonly IConfiguration config;

        public OpenWeatherController(HttpClient htp, ILogger<OpenWeatherController> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

        [HttpGet("/GetOpenWeather/{lat}/{lon}")]
        public async Task<dynamic> GetOpenWeather(double lat, double lon)
        {
            try
            {
                Console.WriteLine(config["ExternalApis:OpenWeatherApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:OpenWeatherKey"])));

                if(lat > 90 || lat < -90)
                {
                    throw new Exception($"latitude must be within the range of -90 to 90 degrees");
                }

                if (lon > 180 || lon < -180)
                {
                    throw new Exception($"longitude must be within the range of -180 to 180 degrees");
                }

                var res = await http.GetFromJsonAsync<System.Text.Json.JsonElement>(config["ExternalApis:OpenWeatherApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:OpenWeatherKey"])));

                if (res.Equals(""))
                {
                    throw new Exception($"No result found for coordinates");
                }

                return ParseToOpenWeather(res.GetProperty("weather")[0], res.GetProperty("main"));
            }
            catch (HttpRequestException e)
            {
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
