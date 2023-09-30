using System.Reflection;
using Mapster;
using SDIA.Entities.User.Responses;

namespace SDIA.Configurations;

public static class MapsterConfiguration
{
    public static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        ConfigureFromStringToAuthResponse(config);
        return services;
    }

    private static void ConfigureFromStringToAuthResponse(TypeAdapterConfig config)
    {
        config.NewConfig<string, AuthResponse>()
            .Map(dest => dest.AccessToken, src => src);
    }
}