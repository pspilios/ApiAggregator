namespace ApiAggregator.Services
{
    public class OpenWeatherClient : IOpenWeatherClient
    {
        private readonly HttpClient http;
        private readonly ILogger<OpenWeatherClient> logger;

        public OpenWeatherClient(HttpClient htp, ILogger<OpenWeatherClient> log)
        {
            http = htp;
            logger = log;
        }

        public async Task<dynamic> GetData()
        {
            return await http.GetFromJsonAsync<dynamic>("https://api.openweathermap.org/data/2.5/weather?lat=90&lon=90&appid=4d5429db1f6ef9d423c56bcca64d2683");
        }
    }
}
