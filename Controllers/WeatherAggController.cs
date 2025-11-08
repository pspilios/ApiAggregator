using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAggController : ControllerBase
    {
        private readonly IWeatherAggService weatherService;

        public WeatherAggController(IWeatherAggService was)
        {
            weatherService = was;
        }

        [HttpGet("/GetAggWeather/{cityName}")]
        public async Task<dynamic> GetAggWeatherByName(string cityName)
        {
            var result = await weatherService.GetAggWeatherByName(cityName);

            return Ok(result);
        }
    }
}
