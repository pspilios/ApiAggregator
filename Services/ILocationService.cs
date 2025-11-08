namespace ApiAggregator.Services
{
    public interface ILocationService
    {
        Task<string> GetCoordinates(string cityname);
    }
}
