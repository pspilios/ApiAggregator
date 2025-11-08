using System.Globalization;

namespace ApiAggregator.Services
{
    public class WeatherAggService: IWeatherAggService
    {
        private readonly HttpClient http;
        private readonly ILogger<WeatherAggService> logger;
        private readonly ILocationService locationService;
        private readonly IOpenMeteoService meteoService;
        private readonly IOpenWeatherService weatherService;
        private readonly ITomorrowService tomorrowService;

        public WeatherAggService(HttpClient htp, ILogger<WeatherAggService> log, ILocationService ls, IOpenMeteoService ms, IOpenWeatherService ws, ITomorrowService ts)
        {
            logger = log;
            http = htp;
            locationService = ls;
            meteoService = ms;
            weatherService = ws;
            tomorrowService = ts;
        }

        public async Task<dynamic> GetAggWeatherByName(string cityName)
        {
            try
            {
                string coord = await locationService.GetCoordinates(cityName);

                var parts = coord.Split(',');

                double lat = double.Parse(parts[0], CultureInfo.InvariantCulture);
                double lon = double.Parse(parts[1], CultureInfo.InvariantCulture);

                var meteoTask = meteoService.GetOpenMeteo(lat, lon);
                var weatherTask = weatherService.GetOpenWeather(lat, lon);
                var tomorrowTask = tomorrowService.GetTomorrow(lat, lon);

                await Task.WhenAll(meteoTask, weatherTask, tomorrowTask);

                return new
                {
                    OpenMeteo = meteoTask.Result,
                    OpenWeather = weatherTask.Result,
                    Tomorrow = tomorrowTask.Result
                };
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
