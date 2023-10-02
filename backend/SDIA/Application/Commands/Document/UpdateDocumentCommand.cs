using Application.DTOs;
using MediatR;

namespace Application.Commands.Document;

public record UpdateDocumentCommand : IRequest<DocumentDto>
{
    public Guid Id { get; init; }
    public string StorageUrl { get; init; } = null!;
}