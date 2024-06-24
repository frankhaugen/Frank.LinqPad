using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINQPadQuery;

public static class JsonUtil
{
    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    public static JsonSerializerOptions Options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {
            new JsonStringEnumConverter(),
            new JsonDirectoryInfoConverter(),
            new JsonMailAddressConverter()
        },
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
	
	
}

public class JsonMailAddressConverter : JsonConverter<MailAddress>
{
    public override MailAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string address = reader.GetString();
        return new MailAddress(address);
    }

    public override void Write(Utf8JsonWriter writer, MailAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class JsonDirectoryInfoConverter : JsonConverter<DirectoryInfo>
{
    public override DirectoryInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string path = reader.GetString();
        return new DirectoryInfo(Path.GetFullPath(path));
    }

    public override void Write(Utf8JsonWriter writer, DirectoryInfo value, JsonSerializerOptions options)
    {
        // Convert to a Unix-like path format (which also works on Windows)
        string normalizedPath = value.FullName.Replace("\\", "/");
        writer.WriteStringValue(normalizedPath);
    }
}