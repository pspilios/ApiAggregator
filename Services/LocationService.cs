using Microsoft.Extensions.Caching.Memory;

namespace ApiAggregator.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient http;
        private readonly IMemoryCache cache;
        private readonly ILogger<LocationService> logger;
        private readonly IConfiguration config;

        public LocationService(HttpClient htp, IMemoryCache mem, ILogger<LocationService> log, IConfiguration conf)
        {
            http = htp;
            cache = mem;
            logger = log;
            config = conf;
        }

        public async Task<string> GetCoordinates(string cityName)
        {
            try
            {
                string cacheKey = $"coord_{cityName}";

                if (!cache.TryGetValue(cacheKey, out System.Text.Json.JsonElement[] res))
                {
                    res = await http.GetFromJsonAsync<System.Text.Json.JsonElement[]>(config["ExternalApis:GeocoderApi"]
                                           .Replace("{name}", Uri.EscapeDataString(cityName))
                                           .Replace("{key}", Uri.EscapeDataString(config["ApiKeys:OpenWeatherKey"])));

                    if (res.Equals("") || res.Length == 0)
                    {
                        throw new Exception($"No result found for city name");
                    }

                    cache.Set(cacheKey, res, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
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
