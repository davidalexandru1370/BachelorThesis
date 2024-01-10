using Serilog;

namespace SDIA.Configurations;

public static class HostConfiguration
{
    public static void ConfigureHost(this ConfigureHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog();
    }
}