namespace ApiAggregator.Services
{
    public interface IOpenMeteoService
    {
        Task<dynamic> GetOpenMeteo(double lat, double lon);
    }
}
