using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService ls)
        {
            locationService = ls;
        }

        [HttpGet("{cityName}")]
        public async Task<dynamic> GetCoordinates(string cityName)
        {
            var result = await locationService.GetCoordinates(cityName);

            return Ok(result);
        }
    }
}
