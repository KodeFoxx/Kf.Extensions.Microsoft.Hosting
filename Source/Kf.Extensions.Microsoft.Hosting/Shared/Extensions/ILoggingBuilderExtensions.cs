using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Kf.Extensions.Microsoft.Hosting.Shared.Extensions
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddAndConfigureSerilog(
            this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            loggingBuilder.AddSerilog(dispose: true);

            return loggingBuilder;
        }
    }
}
