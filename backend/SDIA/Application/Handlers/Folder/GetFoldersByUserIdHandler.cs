using Application.DTOs;
using Application.Interfaces;
using Application.Query.Folder;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Folder;

public class GetFoldersByUserIdHandler : IRequestHandler<GetFoldersByUserIdQuery, List<FolderDto>>
{
    private readonly ISdiaDbContext _dbContext;

    public GetFoldersByUserIdHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FolderDto>> Handle(GetFoldersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var folders = await _dbContext.Folders
            .AsNoTracking()
            .Where(f => f.UserId == request.UserId || f.User.Sid == request.Sid)
            .Include(f => f.Documents)
            .ToListAsync(cancellationToken);
        var foldersDto = folders.Adapt<List<FolderDto>>();

        return foldersDto;
    }
}