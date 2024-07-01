using LINQPad;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LINQPadQuery;

public static class AppUtil
{
    public static HostApplicationBuilder GetHostApplicationBuilder(DirectoryInfo? contentRoot = null)
    {
        contentRoot ??= new FileInfo(Util.CurrentQueryPath).Directory;
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings()
        {
            ContentRootPath = contentRoot?.FullName
        });
        builder.Logging.ClearProviders().AddProvider(new LinqPadLoggerProvider());
        return builder;
    }
}