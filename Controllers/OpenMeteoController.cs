using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static ApiAggregator.Models.OpenMeteo;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenMeteoController : ControllerBase
    {
        private readonly HttpClient http;
        private readonly ILogger<OpenMeteoController> logger;
        private readonly IConfiguration config;

        public OpenMeteoController(HttpClient htp, ILogger<OpenMeteoController> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

        [HttpGet("/GetOpenMeteo/{lat}/{lon}")]
        public async Task<dynamic> GetOpenMeteo(double lat, double lon)
        {
            try
            {
                Console.WriteLine(config["ExternalApis:OpenMeteoApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture)));

                if (lat > 90 || lat < -90)
                {
                    throw new Exception($"latitude must be within the range of -90 to 90 degrees");
                }

                if (lon > 180 || lon < -180)
                {
                    throw new Exception($"longitude must be within the range of -180 to 180 degrees");
                }

                var res = await http.GetFromJsonAsync<System.Text.Json.JsonElement>(config["ExternalApis:OpenMeteoApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture)));

                if (res.Equals(""))
                {
                    throw new Exception($"No result found for coordinates");
                }

                //return res.GetProperty("current");
                return ParseToOpenMeteo(res.GetProperty("current"), res.GetProperty("current_units"));
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
