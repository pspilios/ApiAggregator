using Microsoft.AspNetCore.Mvc;
using ApiAggregator.Services;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeatherController : ControllerBase
    {
        private readonly IOpenWeatherService weatherService;

        public OpenWeatherController(IOpenWeatherService ws)
        {
            weatherService = ws;
        }

        [HttpGet("/GetOpenWeather/{lat}/{lon}")]
        public async Task<dynamic> GetOpenWeather(double lat, double lon)
        {
            var result = await weatherService.GetOpenWeather(lat, lon);

            return Ok(result);
        }
    }
}
