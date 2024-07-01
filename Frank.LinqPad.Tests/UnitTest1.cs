using System.ComponentModel;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Xunit.Abstractions;

namespace LINQPadQuery;

public class JsonTypeConverterTests
{
    private readonly ITestOutputHelper _outputHelper;

    public JsonTypeConverterTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Test1()
    {
        var data = new Dictionary<string, object?>
        {
            { "Name", "Frank" },
            { "Age", 30 },
            { "IsAdmin", true },
            { "Created", new DateTime(2021, 1, 1) },
            { "Null", null },
            { "Array", new[] { 1, 2, 3 } },
            { "Object", new { Name = "Frank" } },
            // { "Type", GetType() },
            { "Guid", Guid.NewGuid() },
            { "Uri", new Uri("https://www.google.com") },
            { "Enum", ConsoleColor.Red },
            { "DirectoryInfo", new DirectoryInfo(@"C:\") },
            { "FileInfo", new FileInfo(@"C:\file.txt") },
            { "MailAddress", new MailAddress("test@example.com") },
        };
        
        var json = JsonSerializer.Serialize(data, JsonSerializerOptions);
        
        _outputHelper.WriteLine(json);
    }

    [Fact]
    public void Test2()
    {
        var json = """
                   {
                     "Name": "Frank",
                     "Age": 30,
                     "IsAdmin": true,
                     "Created": "2021-01-01T00:00:00",
                     "Null": null,
                     "Array": [
                       1,
                       2,
                       3
                     ],
                     "Object": {
                       "Name": "Frank"
                     },
                     "Type": "System.Text.Json.JsonTypeConverterTests, Frank.LinqPad.Tests",
                     "Guid": "2df11cd4-be7e-4011-a6bf-ba077c4e522f",
                     "Uri": "https://www.google.com",
                     "Enum": "Red",
                     "DirectoryInfo": "C:/",
                     "FileInfo": "C:/file.txt",
                     "MailAddress": "test@example.com"
                   }
                   """;
        
        var jsonDocument = JsonDocument.Parse(json);
        var root = jsonDocument.RootElement;
        root.GetProperty("Name").GetString().Should().Be("Frank");
        root.GetProperty("Age").GetInt32().Should().Be(30);
        root.GetProperty("IsAdmin").GetBoolean().Should().BeTrue();
        root.GetProperty("Created").GetDateTime().Should().Be(new DateTime(2021, 1, 1));
        root.GetProperty("Null").ValueKind.Should().Be(JsonValueKind.Null);
        root.GetProperty("Array").EnumerateArray().Select(x => x.GetInt32()).Should().BeEquivalentTo(new[] { 1, 2, 3 });
        root.GetProperty("Object").GetProperty("Name").GetString().Should().Be("Frank");
        root.GetProperty("Type").GetString().Should().Be("System.Text.Json.JsonTypeConverterTests, Frank.LinqPad.Tests");
        root.GetProperty("Guid").GetGuid().Should().Be(Guid.Parse("2df11cd4-be7e-4011-a6bf-ba077c4e522f"));
        root.GetProperty("Uri").GetUri().Should().Be(new Uri("https://www.google.com"));
        root.GetProperty("Enum").GetEnum<ConsoleColor>()!.Should().Be(ConsoleColor.Red);
        root.GetProperty("DirectoryInfo").GetDirectoryInfo().FullName.Should().Be(@"C:\");
        root.GetProperty("FileInfo").GetFileInfo().FullName.Should().Be(@"C:\file.txt");
        root.GetProperty("MailAddress").GetMailAddress().Address.Should().Be("test@example.com");
    }



    private static JsonSerializerOptions JsonSerializerOptions =>
        new()
        {
            WriteIndented = true,
            Converters =
            {
                new JsonTypeConverter(),
                new JsonStringEnumConverter(),
                new JsonDirectoryInfoConverter(),
                new JsonFileInfoConverter(),
                new JsonMailAddressConverter(),
            }
        };    
}