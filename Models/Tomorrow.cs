using System.Text.Json.Nodes;
using System.Text.Json;

namespace ApiAggregator.Models
{
    public class Tomorrow
    {
        public static JsonElement ParseToTomorrow(JsonElement data)
        {
            JsonObject result = new JsonObject();

            foreach (var prop in data.GetProperty("values").EnumerateObject())
            {
                result[prop.Name] = JsonNode.Parse(prop.Value.ToString());
            }

            JsonObject root = new JsonObject
            {
                ["Tomorrow"] = result
            };

            using var doc = JsonDocument.Parse(root.ToJsonString());
            return doc.RootElement.Clone();
        }
    }
}
