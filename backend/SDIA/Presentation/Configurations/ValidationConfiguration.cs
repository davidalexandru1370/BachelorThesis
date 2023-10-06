using Application.Behaviours;
using MediatR;

namespace SDIA.Configurations;

public static class ValidationConfiguration
{
    public static IServiceCollection ConfigureValidations(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}