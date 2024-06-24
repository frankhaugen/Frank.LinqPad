using Microsoft.Extensions.Logging;

namespace LINQPadQuery;

public class LinqPadLoggerFactory : ILoggerFactory
{
    private ILoggerProvider _provider = new LinqPadLoggerProvider();
    
    public void AddProvider(ILoggerProvider provider)
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _provider.CreateLogger(categoryName);
    }

    public void Dispose()
    {
    }
}