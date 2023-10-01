using Application.Commands.Folder;
using Application.DTOs;
using Application.Interfaces;
using Domain.Constants;
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

        folder.Documents.ForEach(d => d.DocumentType = DocumentType.NOT_COMPUTED);

        await _dbContext.Folders.AddAsync(folder);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return folder.Adapt<FolderDto>();
    }
}