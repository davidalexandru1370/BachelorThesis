using Application.Commands.Folder;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Constants;
using Mapster;
using MediatR;

namespace Application.Handlers.Folder;

public class CreateFolderHandler : IRequestHandler<CreateFolderCommand, FolderDto>
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IImageService _imageService;
    private readonly IDocumentService _documentService;

    public CreateFolderHandler(ISdiaDbContext dbContext, IImageService imageService, IDocumentService documentService)
    {
        _dbContext = dbContext;
        _imageService = imageService;
        _documentService = documentService;
    }

    public async Task<FolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = request.Adapt<Domain.Entities.Folder>();

        await _dbContext.Folders.AddAsync(folder, cancellationToken);

        for (int index = 0; index < folder.Documents.Count; index++)
        {
            request.Documents[index].Id = folder.Documents[index].Id;
        }

        var uris = new Dictionary<Guid, string>();

        await Parallel.ForEachAsync(request.Documents, cancellationToken, async (d, forEachCancellationToken) =>
        {
            // var uri = await _imageService.UploadImageAsync(folder.Id, d.Id!.Value, d.File,
            //     forEachCancellationToken);
            // uris[d.Id!.Value] = uri;
            d.DocumentType = await _documentService.AnalyzeDocumentAsync(d.File);
        });

        foreach (var document in folder.Documents)
        {
            document.StorageUrl = uris[document.Id];
        }

        return folder.Adapt<FolderDto>();
    }
}