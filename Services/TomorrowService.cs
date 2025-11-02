using System.Globalization;
using static ApiAggregator.Models.Tomorrow;

namespace ApiAggregator.Services
{
    public class TomorrowService : ITomorrowService
    {
        private readonly HttpClient http;
        private readonly ILogger<TomorrowService> logger;
        private readonly IConfiguration config;

        public TomorrowService(HttpClient htp, ILogger<TomorrowService> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

        public async Task<dynamic> GetTomorrow(double lat, double lon)
        {
            try
            {
                if (config.GetValue<bool>("Debug")) Console.WriteLine(config["ExternalApis:TomorrowApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:TomorrowKey"])));

                if (lat > 90 || lat < -90)
                {
                    throw new Exception($"latitude must be within the range of -90 to 90 degrees");
                }

                if (lon > 180 || lon < -180)
                {
                    throw new Exception($"longitude must be within the range of -180 to 180 degrees");
                }

                var res = await http.GetFromJsonAsync<System.Text.Json.JsonElement>(config["ExternalApis:TomorrowApi"]
                                       .Replace("{lat}", lat.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{lon}", lon.ToString(CultureInfo.InvariantCulture))
                                       .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:TomorrowKey"])));

                if (res.Equals(""))
                {
                    throw new Exception($"No result found for coordinates");
                }

                return ParseToTomorrow(res.GetProperty("data"));
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
