namespace ApiAggregator.Services
{
    public interface IOpenWeatherService
    {
        Task<dynamic> GetOpenWeather(double lat, double lon);
    }
}
