using System.Text;

namespace LINQPadQuery;

/// <summary>
/// A utility class for generating constants.
/// </summary>
public static class ConstantsUtil
{
    /// <summary>
    /// Generates a class with constants from the source collection.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="nameSelector"></param>
    /// <param name="className"></param>
    /// <param name="classNamespace"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static string GenerateConstants<T, T2>(IEnumerable<T> source, Func<T, T2> selector, Func<T, string> nameSelector, string? className = null, string? classNamespace = null)
        => GenerateConstantsInternal(source, selector, nameSelector, className, classNamespace);
    
    /// <summary>
    /// Generates a class with constants from the source collection.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="nameSelector"></param>
    /// <param name="className"></param>
    /// <param name="classNamespace"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GenerateConstants<T>(IEnumerable<T> source, Func<T, string> nameSelector, string? className = null, string? classNamespace = null)
        => GenerateConstantsInternal(source, x => x, nameSelector, className, classNamespace);

    private static string GenerateConstantsInternal<T, T2>(IEnumerable<T> source, Func<T, T2> selector, Func<T, string> nameSelector, string? className = null, string? classNamespace = null)
    {
        className ??= typeof(T).Name;
        var classBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(classNamespace))
            classBuilder.AppendLine($"namespace {classNamespace};");
        classBuilder.AppendLine($"public static class {className}");
        classBuilder.AppendLine("{");
        foreach (var item in source)
        {
            classBuilder.AppendLine($"    public const {GetLongTypeName(typeof(T2))} {selector(item)} = \"{selector(item)}\";");
        }
        classBuilder.AppendLine("}");
        return classBuilder.ToString();
    }

    private static string GetLongTypeName(Type type)
    {
        var name = type switch
        {
            not null when type == typeof(string) => "string",
            not null when type == typeof(int) => "int",
            not null when type == typeof(long) => "long",
            not null when type == typeof(short) => "short",
            not null when type == typeof(byte) => "byte",
            not null when type == typeof(bool) => "bool",
            not null when type == typeof(float) => "float",
            not null when type == typeof(double) => "double",
            not null when type == typeof(decimal) => "decimal",
            not null when type == typeof(char) => "char",
            _ => type?.Name
        };
        
        return $"{type?.Namespace}.{name}";
    }
}