using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace LINQPadQuery;

/// <summary>
/// Utility class for CSV serialization and deserialization.
/// </summary>
public static class CsvUtil
{
    /// <summary>
    /// Deserializes a CSV data string into a list of objects.
    /// </summary>
    /// <typeparam name="TClass">The type of objects to deserialize.</typeparam>
    /// <typeparam name="TMap">The mapping class that defines the mapping between the CSV columns and the object properties.</typeparam>
    /// <param name="csvData">The CSV data string to deserialize.</param>
    /// <param name="hasHeader">A flag indicating whether the CSV data includes a header row. Default is true.</param>
    /// <param name="delimiter">The delimiter used to separate fields in the CSV data. Default is ";".</param>
    /// <param name="newLine">The string representation of a new line in the CSV data. Default is "\n".</param>
    /// <returns>A list of objects deserialized from the CSV data string.</returns>
    public static List<TClass> Deserialize<TClass, TMap>(string csvData, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = CreateConfiguration<TClass, TMap>(hasHeader, delimiter, newLine);

        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, configuration);
        
        csv.Context.RegisterClassMap<TMap>();

        var records = csv.GetRecords<TClass>().ToList();
        return records;
    }

    /// <summary>
    /// Serializes the sequence of objects into a CSV data string.
    /// </summary>
    /// <typeparam name="TClass">The type of objects to serialize.</typeparam>
    /// <typeparam name="TMap">The mapping class that defines the mapping between the object properties and the CSV columns.</typeparam>
    /// <param name="records">The sequence of objects to serialize.</param>
    /// <param name="hasHeader">A flag indicating whether the CSV data should include a header row. Default is true.</param>
    /// <param name="delimiter">The delimiter to use to separate fields in the CSV data. Default is ";".</param>
    /// <param name="newLine">The string representation of a new line in the CSV data. Default is "\n".</param>
    /// <returns>The resulting CSV data string.</returns>
    public static string Serialize<TClass, TMap>(IEnumerable<TClass> records, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = CreateConfiguration<TClass, TMap>(hasHeader, delimiter, newLine);

        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        csv.WriteRecords(records);
        
        return writer.ToString();
    }

    /// <summary>
    /// Asynchronously deserializes the CSV data into a sequence of objects of type TClass.
    /// </summary>
    /// <typeparam name="TClass">The type of objects to deserialize.</typeparam>
    /// <typeparam name="TMap">The mapping class that defines the mapping between the CSV columns and the object properties.</typeparam>
    /// <param name="csvData">The CSV data to deserialize.</param>
    /// <param name="hasHeader">A flag indicating whether the CSV data has a header row. Default is true.</param>
    /// <param name="delimiter">The delimiter used to separate fields in the CSV data. Default is ";".</param>
    /// <param name="newLine">The string used to represent a new line in the CSV data. Default is "\n".</param>
    /// <returns>An asynchronous enumerable that represents the deserialized objects.</returns>
    public static IAsyncEnumerable<TClass> DeserializeAsync<TClass, TMap>(string csvData, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = CreateConfiguration<TClass, TMap>(hasHeader, delimiter, newLine);

        using var reader = new StringReader(csvData);
        using var csv = new CsvReader(reader, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        
        return csv.GetRecordsAsync<TClass>();
    }

    /// <summary>
    /// Serializes a collection of objects into a CSV data string asynchronously.
    /// </summary>
    /// <typeparam name="TClass">The type of objects to serialize.</typeparam>
    /// <typeparam name="TMap">The mapping class that defines the mapping between the object properties and the CSV columns.</typeparam>
    /// <param name="records">The collection of objects to serialize.</param>
    /// <param name="hasHeader">A flag indicating whether the CSV data should include a header row. Default is true.</param>
    /// <param name="delimiter">The delimiter used to separate fields in the CSV data. Default is ";".</param>
    /// <param name="newLine">The string representation of a new line in the CSV data. Default is "\n".</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the CSV data string.</returns>
    public static async Task<string> SerializeAsync<TClass, TMap>(IEnumerable<TClass> records, bool hasHeader = true, string delimiter = ";", string newLine = "\n") where TMap : ClassMap<TClass> where TClass : class
    {
        var configuration = CreateConfiguration<TClass, TMap>(hasHeader, delimiter, newLine);

        await using var writer = new StringWriter();
        await using var csv = new CsvWriter(writer, configuration);
        
        csv.Context.RegisterClassMap<TMap>();
        await csv.WriteRecordsAsync(records);
        
        return writer.ToString();
    }

    private static CsvConfiguration CreateConfiguration<TClass, TMap>(bool hasHeader, string delimiter, string newLine) where TMap : ClassMap<TClass> where TClass : class =>
        new(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = hasHeader,
            NewLine = newLine
        };
}