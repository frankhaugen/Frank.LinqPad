using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINQPadQuery;

public static class JsonUtil
{
    public static T Deserialize<T>(string json, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        jsonSerializerOptions ??= Options;
        return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions)!;
    }

    public static string Serialize<T>(T obj, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        jsonSerializerOptions ??= Options;
        return JsonSerializer.Serialize(obj, jsonSerializerOptions);
    }

    public static JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {
            new JsonStringEnumConverter(),
            new JsonDirectoryInfoConverter(),
            new JsonMailAddressConverter(),
            new JsonFileInfoConverter()
        },
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
}