using Application.Commands.Folder;
using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Folder;

public class DeleteFolderByIdHandler : IRequestHandler<DeleteFolderByIdCommand>
{
    private readonly ISdiaDbContext _dbContext;

    public DeleteFolderByIdHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteFolderByIdCommand request, CancellationToken cancellationToken)
    {
        var folder = await _dbContext.Folders
            .Include(f => f.Documents)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (folder is null)
        {
            throw new NotFoundException("Folder not found");
        }

        _dbContext.Folders.Remove(folder);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}