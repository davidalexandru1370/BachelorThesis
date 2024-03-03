using SDIA.SignalR.Hubs;

namespace SDIA.Configurations;

public static class SignalRConfiguration
{
    public static IServiceCollection ConfigureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }

    public static void UseSignalR(this WebApplication app, IConfiguration configuration)
    {
        var baseHub = "/api/hubs";
        var createFolderHub = "createFolder";
        app.MapHub<CreateFolderHub>($"{baseHub}/{createFolderHub}/notification");
    }
}