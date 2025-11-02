using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenMeteoController : ControllerBase
    {
        private readonly IOpenMeteoService weatherService;

        public OpenMeteoController(IOpenMeteoService ws)
        {
            weatherService = ws;
        }

        [HttpGet("/GetOpenMeteo/{lat}/{lon}")]
        public async Task<IActionResult> GetOpenMeteo(double lat, double lon)
        {
            var result = await weatherService.GetOpenMeteo(lat, lon);

            return Ok(result);
        }
    }
}
