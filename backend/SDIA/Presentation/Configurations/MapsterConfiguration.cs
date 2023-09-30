using System.Reflection;
using Mapster;
using SDIA.Entities.User.Responses;

namespace SDIA.Configurations;

public static class MapsterConfiguration
{
    public static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        return services;
    }
}

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ConfigureFromStringToAuthResponse(config);
    }

    private void ConfigureFromStringToAuthResponse(TypeAdapterConfig config)
    {
        config.NewConfig<string, AuthResponse>()
            .Map(dest => dest, src => new AuthResponse
            {
                AccessToken = src
            });
    }
}