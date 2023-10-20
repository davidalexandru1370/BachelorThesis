using Application.Configurations;

namespace SDIA.Configurations;

public static class OptionsConfiguration
{
    public static IServiceCollection ConfigureOptionsCollection(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureAzureBlob(services, configuration);
        return services;
    }
    
    private static void ConfigureAzureBlob(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureBlobConfiguration>(configuration.GetSection(nameof(AzureBlobConfiguration)));
        services.Configure<DocumentServiceConfiguration>(configuration.GetSection(nameof(DocumentServiceConfiguration)));
    }
}