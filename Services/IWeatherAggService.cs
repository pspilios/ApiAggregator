namespace ApiAggregator.Services
{
    public interface IWeatherAggService
    {
        Task<dynamic> GetAggWeatherByName(string cityName);
    }
}
