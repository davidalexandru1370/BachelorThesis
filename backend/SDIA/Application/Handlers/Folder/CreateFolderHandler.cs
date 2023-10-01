using Application.Commands.Folder;
using Application.DTOs;
using Application.Interfaces;
using Mapster;
using MediatR;

namespace Application.Handlers.Folder;

public class CreateFolderHandler : IRequestHandler<CreateFolderCommand, FolderDto>
{
    private readonly ISdiaDbContext _dbContext;
    
    public CreateFolderHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<FolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = request.Adapt<Domain.Entities.Folder>();

        await _dbContext.Folders.AddAsync(folder);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return folder.Adapt<FolderDto>();
    }
}