using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiAggregator.Models
{
    public class OpenWeather
    {
        public static JsonElement ParseToOpenWeather(JsonElement weather, JsonElement main)
        {
            JsonObject result = new JsonObject();

            foreach (var prop in weather.EnumerateObject())
            {
                result[prop.Name] = JsonNode.Parse(prop.Value.GetRawText());
            }

            foreach (var prop in main.EnumerateObject())
            {
                result[prop.Name] = JsonNode.Parse(prop.Value.GetRawText());
            }

            using var doc = JsonDocument.Parse(result.ToJsonString());
            return doc.RootElement.Clone();
        }
    }
}
