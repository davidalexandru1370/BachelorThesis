using Application.DTOs;
using Application.Interfaces;
using Application.Query.Folder;
using Domain.Constants.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Folder;

public class GetFolderByIdHandler : IRequestHandler<GetFolderByIdQuery, FolderDto>
{
    private readonly ISdiaDbContext _dbContext;

    public GetFolderByIdHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FolderDto> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
    {
        var folder = await _dbContext.Folders
            .Include(f => f.Documents)
            .Include(f => f.Errors)
            .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

        if (folder is null)
        {
            throw new NotFoundException(I18N.DoesNotExist);
        }

        return folder.Adapt<FolderDto>();
    }
}