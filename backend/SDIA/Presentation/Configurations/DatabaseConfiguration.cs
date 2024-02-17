using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace SDIA.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<SdiaDbContext>(configuration.GetConnectionString("SDIA"));
        ApplyMigrations(services);
        return services;
    }

    private static void ApplyMigrations(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SdiaDbContext>();
        dbContext.Database.Migrate();
    }
}