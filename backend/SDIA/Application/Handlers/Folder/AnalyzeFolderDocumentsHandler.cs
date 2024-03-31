using Application.Commands.Folder;
using Application.DTOs;
using Application.Interfaces;
using Domain.Constants.Enums;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Handlers.Folder;

public class AnalyzeFolderDocumentsHandler : IRequestHandler<AnalyzeFolderDocumentsCommand, AnalyzeFolderDto>
{
    private readonly ISdiaDbContext _dbContext;

    private static readonly Dictionary<FolderType, HashSet<DocumentType>> DocumentsForFolder = new()
    {
        {
            FolderType.CarNeverRegistered, new HashSet<DocumentType>
            {
                DocumentType.OwnershipContract,
                DocumentType.IdentityCard
            }
        },
        {
            FolderType.CarFromAnotherCountry, new HashSet<DocumentType>
            {
                DocumentType.OwnershipContract,
                DocumentType.IdentityCard,
            }
        },
        {
            FolderType.AlreadyRegisteredVehicleInCountry, new HashSet<DocumentType>
            {
                DocumentType.OwnershipContract,
                DocumentType.IdentityCard,
                DocumentType.UnregisterVehicle
            }
        }
    };

    public AnalyzeFolderDocumentsHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AnalyzeFolderDto> Handle(AnalyzeFolderDocumentsCommand request,
        CancellationToken cancellationToken)
    {
        var folder = await _dbContext.Folders.FindAsync(request.FolderId);
        
        if (folder is null)
        {
            throw new NotFoundException(I18N.DoesNotExist);
        }

        var response = new AnalyzeFolderDto
        {
            FolderId = request.FolderId,
            IsCorrect = false,
            Errors = new List<string>()
        };

        var errors = ValidateDocuments(request.FolderType, new HashSet<DocumentType>(request.Documents));
        if (errors.Count == 0)
        {
            folder.IsCorrect = true;
            response.IsCorrect = true;
        }
        else
        {
            response.Errors = errors;
        }

        await Parallel.ForEachAsync(errors, cancellationToken, async (error, forEachCancellationToken) =>
        {
            await _dbContext.FolderErrors.AddAsync(new FolderErrors
            {
                FolderId = request.FolderId,
                Error = error
            }, forEachCancellationToken);
        });

        await _dbContext.SaveChangesAsync(cancellationToken);

        return response;
    }

    private List<String> ValidateDocuments(FolderType folderType, HashSet<DocumentType> documents)
    {
        List<String> errors = new();

        foreach (var document in DocumentsForFolder[folderType])
        {
            if (!documents.Contains(document))
            {
                switch (document)
                {
                    case DocumentType.IdentityCard:
                        errors.Add(I18N.IdentityCardDoesNotExist.ToString());
                        break;
                    case DocumentType.OwnershipContract:
                        errors.Add(I18N.OwnershipContractDoesNotExist.ToString());
                        break;
                    case DocumentType.UnregisterVehicle:
                        errors.Add(I18N.UnregisterVehicleDoesNotExist.ToString());
                        break;
                    case DocumentType.NotFound:
                        errors.Add(I18N.TooManyDocuments.ToString());
                        break;
                }
            }
        }

        return errors;
    }
}