using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LINQPadQuery;

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