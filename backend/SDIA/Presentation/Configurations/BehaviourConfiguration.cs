using Application.Behaviours;
using MediatR;

namespace SDIA.Configurations;

public static class BehaviourConfiguration
{
    public static IServiceCollection RegisterBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}