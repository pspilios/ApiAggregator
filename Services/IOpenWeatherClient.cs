namespace ApiAggregator.Services
{
    public interface IOpenWeatherClient
    {
        Task<dynamic> GetData();
    }
}
