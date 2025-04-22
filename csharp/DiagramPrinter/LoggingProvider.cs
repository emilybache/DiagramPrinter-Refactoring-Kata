using Microsoft.Extensions.Logging;

namespace DiagramPrinter;

public class LoggingProvider
{
    public static readonly ILoggerFactory DiagramLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("DiagramPrinter", LogLevel.Information)
            .AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    });

    public static ILogger<T> CreateLogger<T>() => DiagramLoggerFactory.CreateLogger<T>();
}