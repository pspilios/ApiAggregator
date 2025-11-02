namespace ApiAggregator.Models
{
    public class CurrentWeather
    {
        public Location loc { get; set; }

        public double windSpeed { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);

        public string? weatherDesc { get; set; }
    }
}
