using Application.Commands.Folder;
using Application.DTOs;
using Domain.Constants.Enums;
using MediatR;

namespace Application.Handlers.Folder;

public class AnalyzeFolderDocumentsHandler : IRequestHandler<AnalyzeFolderDocumentsCommand, AnalyzeFolderDto>
{
    public Task<AnalyzeFolderDto> Handle(AnalyzeFolderDocumentsCommand request, CancellationToken cancellationToken)
    {
        var response = new AnalyzeFolderDto
        {
            FolderId = request.FolderId,
            IsCorrect = false,
            Errors = new List<string>()
        };

        switch (request.FolderType)
        {
            case FolderType.CarNeverRegistered:
                
                break;
            case FolderType.CarFromAnotherCountry:
                break;
            case FolderType.AlreadyRegisteredVehicleInCountry:
                break;
        }

        return Task.FromResult(response);
    }
}