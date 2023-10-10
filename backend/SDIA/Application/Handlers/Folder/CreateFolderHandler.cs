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
    
    public CreateFolderHandler(ISdiaDbContext dbContext, IImageService imageService)
    {
        _dbContext = dbContext;
        _imageService = imageService;
    }

    public async Task<FolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = request.Adapt<Domain.Entities.Folder>();

        await _dbContext.Folders.AddAsync(folder);
        
        await Parallel.ForEachAsync(request.Documents,async (d, _) =>
        {
            await _imageService.UploadImageAsync(folder.Id, d.File, cancellationToken);
            d.DocumentType = DocumentType.NotComputed;
        });
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return folder.Adapt<FolderDto>();
    }
}