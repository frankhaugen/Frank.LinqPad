using System.Net.Mail;
using System.Text.Json;

namespace LINQPadQuery;

public static class JsonElementExtensions
{
    public static DirectoryInfo GetDirectoryInfo(this JsonElement element) => new(element.GetString());

    public static MailAddress GetMailAddress(this JsonElement element) => new MailAddress(element.GetString());

    public static FileInfo GetFileInfo(this JsonElement element) => new(element.GetString());

    public static Type GetType(this JsonElement element) => Type.GetType(element.GetString())!;
    
    public static Uri GetUri(this JsonElement element) => new(element.GetString());
    
    public static T GetEnum<T>(this JsonElement element) where T : struct, Enum => Enum.Parse<T>(element.GetString());
}
