namespace ApiAggregator.Services
{
    public interface ITomorrowService
    {
        Task<dynamic> GetTomorrow(double lat, double lon);
    }
}
