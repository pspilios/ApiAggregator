namespace ApiAggregator.Models
{
    public class CurrentWeather
    {
        static readonly Dictionary<int, string> weatherCodes = new()
        {
            { 0, "Clear Sky" },
            { 1, "Mainly Clear" },
            { 2, "Partly Cloudy" },
            { 3, "Overcast" },
            { 45, "Fog" },
            { 48, "Rime Fog" },
            { 51, "Light Drizzle" },
            { 53, "Moderate Drizzle" },
            { 55, "Dense Drizzle" },
            { 56, "Light Freezing Drizzle" },
            { 57, "Dense Freezing Drizzle" },
            { 61, "Slight Rain" },
            { 63, "Moderate Rain" },
            { 65, "Heavy Rain" },
            { 66, "Light Freezing Rain" },
            { 67, "Heavy Freezing Rain" },
            { 71, "Slight Snow Fall" },
            { 73, "Moderate Snow Fall" },
            { 75, "Heavy Snow Fall" },
            { 77, "Snow Grains" },
            { 80, "Slight Rain Shower" },
            { 81, "Moderate Rain Shower" },
            { 82, "Heavy Rain Shower" },
            { 85, "Slight Snow Shower" },
            { 86, "Heavy Snow Shower" },
            { 95, "Slight-Moderate Thunderstorm" },
            { 96, "Thunderstorm with slight hail" },
            { 99, "Thunderstorm with heady hail" }
        };

        public Location loc { get; set; }

        public double windSpeed { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);

        public string? weatherDesc { get; set; }
    }
}
