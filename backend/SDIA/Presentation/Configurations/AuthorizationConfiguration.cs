using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SDIA.Security.TokenValidators;

namespace SDIA.Configurations;

public static class AuthorizationConfiguration
{
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Normal", options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                    ValidateIssuerSigningKey = true,
                };
            }).AddJwtBearer("Google", options =>
            {
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new GoogleTokenValidator()
                {
                    ClientId = configuration["Authentication:Google:ClientId"],
                    ClientSecret = configuration["Authentication:Google:ClientSecret"]
                });
            });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("Normal", "Google")
                .Build();
        });


        return services;
    }
}