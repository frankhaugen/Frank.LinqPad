namespace LINQPadQuery;

public static class CancellationTokenUtil
{
    public static CancellationToken Get(TimeSpan duration) => new CancellationTokenSource(duration).Token;
    public static CancellationTokenSource GetSource(TimeSpan duration) => new CancellationTokenSource(duration);
}