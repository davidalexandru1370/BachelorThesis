using Application.Behaviours;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Application.SignalR;
using Application.Utilities;
using FluentValidation;
using Infrastructure.DbContext;
using MediatR;

namespace SDIA.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<ISdiaDbContext, SdiaDbContext>();

        services.AddTransient<IJwtUtilities, JwtUtilities>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IDocumentService, DocumentService>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddSingleton<ICreateFolderNotification, CreateFolderNotification>();

        services.AddValidatorsFromAssembly(Application.Gatherings.ApplicationAssembly.Assembly,
            includeInternalTypes: true);

        return services;
    }
}