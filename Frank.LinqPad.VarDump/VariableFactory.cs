using VarDump;
using VarDump.Visitor;

namespace LINQPadQuery;

internal static class VariableFactory
{
    public static string DumpVar<T>(T obj, DumpOptions? options = null)
    {
        options ??= DumpOptionsFactory.Create();
        var dumper = new CSharpDumper(options);
        var result = dumper.Dump(obj);
        return result;
    }
}