using Microsoft.OpenApi.Models;

namespace SDIA.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
       services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new OpenApiInfo { 
               Title = "SDIA API",
               Version = "v1" 
           });
           c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
               In = ParameterLocation.Header, 
               Description = "Please insert JWT with Bearer into field",
               Name = "Authorization",
               Scheme = "Bearer",
               Type = SecuritySchemeType.Http 
           });
           c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               { 
                   new OpenApiSecurityScheme 
                   { 
                       Reference = new OpenApiReference 
                       { 
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer" 
                       } 
                   },
                   new string[] { } 
               } 
           });
       });

       return services;
    }
}