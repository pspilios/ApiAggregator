namespace ApiAggregator.Services
{
    public interface IOpenMeteoClient
    {
        Task<dynamic> GetData();
    }
}
