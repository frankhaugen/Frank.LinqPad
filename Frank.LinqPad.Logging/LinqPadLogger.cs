using LINQPad;
using Microsoft.Extensions.Logging;

namespace LINQPadQuery;

public class LinqPadLogger(string categoryName) : ILogger
{
    public IDisposable BeginScope<TState>(TState state) => new LinqPadScope();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var log = new LogThing<TState>(DateTime.UtcNow, categoryName, formatter.Invoke(state, exception), logLevel , eventId, state, exception);
        
        log.Message.Dump();
    }
    
    private record LogThing<TState>(DateTime Timestamp, string CategoryName, string Message, LogLevel LogLevel, EventId EventId, TState State, Exception? Exception);
}