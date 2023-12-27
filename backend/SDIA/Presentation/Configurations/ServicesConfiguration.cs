namespace SDIA.Configurations;

public static class ConfigureService
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddGrpc();
        services.AddHttpClient();

        services.ConfigureSerilog(configuration)
            .ConfigureDatabase(configuration)
            .ConfigureDependencyInjection()
            .ConfigureMediatr()
            .ConfigureMapster()
            .ConfigureAuthorization(configuration)
            .ConfigureSwagger()
            .RegisterBehaviours()
            .ConfigureOptionsCollection(configuration);
    }
}