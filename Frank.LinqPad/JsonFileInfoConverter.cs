using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINQPadQuery;

public class JsonFileInfoConverter : JsonConverter<FileInfo>
{
    public override FileInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string path = reader.GetString();
        return new FileInfo(Path.GetFullPath(path));
    }

    public override void Write(Utf8JsonWriter writer, FileInfo value, JsonSerializerOptions options)
    {
        // Convert to a Unix-like path format (which also works on Windows)
        string normalizedPath = value.FullName.Replace("\\", "/");
        writer.WriteStringValue(normalizedPath);
    }
}