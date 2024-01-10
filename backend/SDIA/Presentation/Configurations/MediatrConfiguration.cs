using Application.Gatherings;

namespace SDIA.Configurations;

public static class MediatrConfiguration
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(ApplicationAssembly.Assembly);
        });
        
        return services;
    }
}