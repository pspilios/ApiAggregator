namespace ApiAggregator.Services
{
    public class OpenMeteoClient : IOpenMeteoClient
    {
        private readonly HttpClient http;
        private readonly ILogger<OpenMeteoClient> logger;

        public OpenMeteoClient(HttpClient htp, ILogger<OpenMeteoClient> log)
        {
            http = htp;
            logger = log;
        }

        public async Task<dynamic> GetData()
        {
            return await http.GetFromJsonAsync<dynamic>("https://api.open-meteo.com/v1/forecast?latitude=37.9838&longitude=23.7278&current=temperature_2m,weather_code,wind_speed_10m&timezone=Europe%2FMoscow");
        }
    }
}
