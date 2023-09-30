namespace SDIA.Configurations;

public static class ConfigureService
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .ConfigureDatabase(configuration)
            .ConfigureDependencyInjection()
            .ConfigureMediatr();
    }
}