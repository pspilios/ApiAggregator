namespace ApiAggregator.Services
{
    public interface ILocationService
    {
        Task<dynamic> GetCoordinates(string cityname);
    }
}
