using System.Reflection;
using Application.Commands.Folder;
using Application.DTOs;
using Mapster;
using SDIA.Entities.Document.Requests;
using SDIA.Entities.Folder.Requests;
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
        ConfigureFromCreateDocumentRequestToCreateDocumentDto(config);
    }

    private void ConfigureFromStringToAuthResponse(TypeAdapterConfig config)
    {
        config.NewConfig<string, AuthResponse>()
            .MapWith(s => new AuthResponse() { AccessToken = s });
    }

    private void ConfigureFromCreateDocumentRequestToCreateDocumentDto(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDocumentRequest, CreateDocumentDto>()
            .Ignore(src => src.File)
            .AfterMapping((src, dest) =>
            {
                dest.File = src.File;
            });
    }
}