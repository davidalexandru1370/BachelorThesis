using System.Reflection;
using Application.Commands.Folder;
using Application.DTOs;
using Domain.Entities;
using Mapster;
using SDIA.Entities.Document.Requests;
using SDIA.Entities.Document.Responses;
using SDIA.Entities.Folder.Requests;
using SDIA.Entities.Folder.Responses;
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
        ConfigureFromFolderToFolderDto(config);
        ConfigureFromFolderDtoToAnalyzeFolderDocumentsCommand(config);
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
            .AfterMapping((src, dest) => { dest.File = src.File; });
    }

    private void ConfigureFromFolderToFolderDto(TypeAdapterConfig config)
    {
        config.NewConfig<Folder, FolderDto>()
            .Map(dest => dest.Errors,
                src => src.Errors == null ? new List<string>() : src.Errors.Select(e => e.Error).ToList());
    }

    private void ConfigureFromFolderDtoToAnalyzeFolderDocumentsCommand(TypeAdapterConfig config)
    {
        config.NewConfig<FolderDto, AnalyzeFolderDocumentsCommand>()
            .Map(dest => dest.Documents, src => src.Documents.Select(d => d.DocumentType).ToList())
            .Map(dest => dest.FolderId, src => src.Id);
    }
    
    private void ConfigureFromFolderDtoToFolderInfoResponse(TypeAdapterConfig config)
    {
        config.NewConfig<FolderDto, FolderInfoResponse>()
            .Map(dest => dest.Errors, src => src.Errors);
    }
}