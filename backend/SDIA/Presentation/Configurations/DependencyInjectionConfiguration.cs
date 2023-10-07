using Application.Interfaces;
using Application.Utilities;
using Infrastructure.DbContext;

namespace SDIA.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddDbContext<ISdiaDbContext, SdiaDbContext>();
        services.AddTransient<IJwtUtilities, JwtUtilities>();
        return services;
    }
}