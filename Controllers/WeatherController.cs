using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient http;
        private readonly ILogger<WeatherController> logger;
        private readonly IConfiguration config;

        public WeatherController(HttpClient htp, ILogger<WeatherController> log, IConfiguration conf)
        {
            http = htp;
            logger = log;
            config = conf;
        }

        //[HttpGet("/GetAggWeather/{cityname}")]
        //public async Task<dynamic> GetAggWeatherByName(string cityname)
        //{
        //    try
        //    {
        //        //var location = new LocationController.GetCoordinates(cityname);
                
        //        //var res = OpenWeatherController.GetOpenWeather();

        //        return res;
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        Console.WriteLine($"HTTP request failed: {e.Message}");
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"System threw exception: {e.Message}");
        //        return null;
        //    }
        //}
    }
}
