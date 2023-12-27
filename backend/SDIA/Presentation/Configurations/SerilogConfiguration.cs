using Serilog;

namespace SDIA.Configurations;

public static class SerilogConfiguration
{
    public static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        Log.Logger = logger;
        
        return services;
    }
}