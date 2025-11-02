namespace ApiAggregator.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient http;
        private readonly ILogger<LocationService> logger;
        private readonly IConfiguration config;

        public LocationService(HttpClient htp, ILogger<LocationService> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

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

                return res[0].GetProperty("lat").ToString() + "," + res[0].GetProperty("lon").ToString();
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
