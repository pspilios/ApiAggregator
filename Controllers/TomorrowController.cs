using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TomorrowController : ControllerBase
    {
        private readonly ITomorrowService weatherService;

        public TomorrowController(ITomorrowService ws)
        {
            weatherService = ws;
        }

        [HttpGet("/GetTomorrow/{lat}/{lon}")]
        public async Task<dynamic> GetTomorrow(double lat, double lon)
        {
            var result = await weatherService.GetTomorrow(lat, lon);

            return result;
        }
    }
}
