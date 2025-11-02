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

            JsonObject root = new JsonObject
            {
                ["OpenWeather"] = result
            };

            using var doc = JsonDocument.Parse(root.ToJsonString());
            return doc.RootElement.Clone();
        }
    }
}
