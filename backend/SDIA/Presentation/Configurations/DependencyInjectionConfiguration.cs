using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Application.Utilities;
using Infrastructure.DbContext;
using SDIA.SignalR.Hubs;

namespace SDIA.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<ISdiaDbContext, SdiaDbContext>();
        services.AddScoped<ICreateFolderNotification, CreateFolderHub>();
        services.AddTransient<IJwtUtilities, JwtUtilities>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IDocumentService, DocumentService>();

        return services;
    }
}