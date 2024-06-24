using VarDump.Visitor;

namespace LINQPadQuery;

public static class DumpOptionsFactory
{
    public static DumpOptions Create()
    {
        return new DumpOptions()
        {
            IgnoreNullValues = true,
            UseTypeFullName = true,
            Descriptors =
            {
                new PropertySortingMiddleware()
            }
        };
    }
}