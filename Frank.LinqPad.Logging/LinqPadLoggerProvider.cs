using Microsoft.Extensions.Logging;

namespace LINQPadQuery;

public class LinqPadLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new LinqPadLogger(categoryName);
    }

    public void Dispose()
    {
    }
}