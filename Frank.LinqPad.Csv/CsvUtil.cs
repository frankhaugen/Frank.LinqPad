using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace LINQPadQuery;

public static class CsvUtil
{
    public static List<TClass> Deserialize<TClass, TMap>(string csvData, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = hasHeader,
            NewLine = newLine
        };

        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, configuration);
        
        csv.Context.RegisterClassMap<TMap>();

        var records = csv.GetRecords<TClass>().ToList();
        return records;
    }
    
    public static string Serialize<TClass, TMap>(IEnumerable<TClass> records, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = hasHeader,
            NewLine = newLine
        };

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        csv.WriteRecords(records);
        
        return writer.ToString();
    }
    
    // Async methods:
    public static IAsyncEnumerable<TClass> DeserializeAsync<TClass, TMap>(string csvData, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = hasHeader,
            NewLine = newLine
        };

        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        
        return csv.GetRecordsAsync<TClass>();
    }
    
    public static async Task<string> SerializeAsync<TClass, TMap>(IEnumerable<TClass> records, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = hasHeader,
            NewLine = newLine
        };

        await using var writer = new StringWriter();
        await using var csv = new CsvWriter(writer, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        await csv.WriteRecordsAsync(records);
        
        return writer.ToString();
    }
}