using Applicantion.Interfaces;
using Infrastructure.DbContext;

namespace SDIA.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddDbContext<ISdiaDbContext, SdiaDbContext>();
        
        return services;
    }
}