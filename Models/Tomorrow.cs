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
                result[prop.Name] = ConvertToJsonNode(prop.Value);
            }

            using var doc = JsonDocument.Parse(result.ToJsonString());
            return doc.RootElement.Clone();
        }
        private static JsonNode ConvertToJsonNode(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var obj = new JsonObject();
                    foreach (var p in element.EnumerateObject())
                        obj[p.Name] = ConvertToJsonNode(p.Value);
                    return obj;

                case JsonValueKind.Array:
                    var arr = new JsonArray();
                    foreach (var item in element.EnumerateArray())
                        arr.Add(ConvertToJsonNode(item));
                    return arr;

                case JsonValueKind.String:
                    return JsonValue.Create(element.GetString());

                case JsonValueKind.Number:
                    return JsonValue.Create(element.GetDouble()); // or GetInt32/GetInt64 if appropriate

                case JsonValueKind.True:
                case JsonValueKind.False:
                    return JsonValue.Create(element.GetBoolean());

                case JsonValueKind.Null:
                default:
                    return null;
            }
        }
    }
}
