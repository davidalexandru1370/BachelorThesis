using Application.Commands.Folder;
using Application.DTOs;
using Application.Entities.Response;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.SignalR;
using Domain.Constants;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Handlers.Folder;

public class CreateFolderHandler : IRequestHandler<CreateFolderCommand, FolderDto>
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IImageService _imageService;
    private readonly IDocumentService _documentService;
    private readonly ICreateFolderNotification _createFolderNotification;
    private readonly Mutex _mutex = new();  
    public CreateFolderHandler(ISdiaDbContext dbContext,
        IImageService imageService,
        IDocumentService documentService,
        ICreateFolderNotification createFolderNotification)
    {
        _dbContext = dbContext;
        _imageService = imageService;
        _documentService = documentService;
        _createFolderNotification = createFolderNotification;
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
        int uploadedDocuments = 0;
        int analyzedDocuments = 0;
        var creatingFolderNotification = new CreateFolderNotificationResponse();
        await Parallel.ForEachAsync(request.Documents, cancellationToken, 
            async (d, forEachCancellationToken) =>
        {
            var name = $"{folder.Id}/{d.Id!.Value}";
            var uri = await _imageService.UploadImageAsync(name, d.File,
                forEachCancellationToken);
            Interlocked.Increment(ref uploadedDocuments);
            if (uploadedDocuments == request.Documents.Count)
            {
                _mutex.WaitOne();
                creatingFolderNotification.ImagesUploaded = true;
                await _createFolderNotification.SendNewStatus(creatingFolderNotification, 
                    folder.UserId,
                    cancellationToken);
                _mutex.ReleaseMutex();
            }

            uris[d.Id!.Value] = uri;
            d.DocumentType = await _documentService.AnalyzeDocumentAsync(d.File);
            Interlocked.Increment(ref analyzedDocuments);
            if (analyzedDocuments == request.Documents.Count)
            {
                
                creatingFolderNotification.DocumentsAnalyzed = true;
                await _createFolderNotification.SendNewStatus(creatingFolderNotification,
                    folder.UserId,
                    cancellationToken);
            }
        });

        for (int index = 0; index < folder.Documents.Count; index++)
        {
            folder.Documents[index].DocumentType = request.Documents[index].DocumentType;
            folder.Documents[index].StorageUrl = uris[folder.Documents[index].Id];
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return folder.Adapt<FolderDto>();
    }
}