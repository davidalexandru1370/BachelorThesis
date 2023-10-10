using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Application.Utilities;
using Infrastructure.DbContext;
using Serilog;
using Serilog.Extensions.Logging;

namespace SDIA.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddHttpContextAccessor(); 
        
        services.AddDbContext<ISdiaDbContext, SdiaDbContext>();
        
        services.AddTransient<IJwtUtilities, JwtUtilities>();
        services.AddTransient<IImageService, ImageService>();

        services.AddSingleton(Log.Logger);
        
        return services;
    }
}