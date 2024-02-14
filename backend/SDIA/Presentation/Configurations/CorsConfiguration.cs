namespace SDIA.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration = default)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                       builder => builder
                                                              .AllowAnyMethod()
                                                                                     .AllowAnyHeader()
                                                                                                            .SetIsOriginAllowed((host) => true)
                                                                                                                                   .AllowCredentials());
            });

            return services;
        }
    }
}
