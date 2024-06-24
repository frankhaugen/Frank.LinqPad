using System.Text;

namespace LINQPadQuery;

/// <summary>
/// A utility class for generating enums.
/// </summary>
public static class EnumUtil
{
    /// <summary>
    /// Generates an enum from the source collection.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="nameSelector"></param>
    /// <param name="enumName"></param>
    /// <param name="enumNamespace"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GenerateEnum<T>(IEnumerable<T> source, Func<T, string> nameSelector, string? enumName = null, string? enumNamespace = null)
    {
        enumName ??= typeof(T).Name;
        var enumBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(enumNamespace))
            enumBuilder.AppendLine($"namespace {enumNamespace};");
        enumBuilder.AppendLine($"public enum {enumName}");
        enumBuilder.AppendLine("{");
        foreach (var item in source)
        {
            enumBuilder.AppendLine($"    {nameSelector(item)},");
        }
        enumBuilder.AppendLine("}");
        return enumBuilder.ToString();
    }
}