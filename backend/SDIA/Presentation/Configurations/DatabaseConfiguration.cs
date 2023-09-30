using Infrastructure.DbContext;

namespace SDIA.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<SdiaDbContext>(configuration.GetConnectionString("SDIA"));
        
        return services;
    }
}