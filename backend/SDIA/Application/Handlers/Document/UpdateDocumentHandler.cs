using Application.Commands.Document;
using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Document;

public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand, DocumentDto>
{
    private readonly ISdiaDbContext _dbContext;

    public UpdateDocumentHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DocumentDto> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _dbContext.Documents
            .Include(d => d.Folder)
            .FirstOrDefaultAsync(f => f.Id == request.Id);

        if (document is null)
        {
            throw new NotFoundException("Document not found");
        }

        if (document.Folder.UserId != request.OwnerId)
        {
            throw new ForbiddenException("You are not allowed to update this document");
        }

        document.StorageUrl = request.StorageUrl;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return document.Adapt<DocumentDto>();
    }
}