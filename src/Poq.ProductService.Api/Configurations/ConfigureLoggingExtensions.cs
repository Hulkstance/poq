using Serilog;
using Serilog.Events;

namespace Poq.ProductService.Api.Configurations;

internal static class ConfigureLoggingExtensions
{
    public static void ConfigureLogging(this IHostBuilder builder)
    {
        const string outputTemplate =
            "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";

        builder.UseSerilog((_, cfg) =>
            cfg
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: outputTemplate));
    }
}
