using Application.DTOs;
using Application.Interfaces;
using Domain.Constants.Enums;
using MediatR;

namespace Application.Commands.Folder;

public record CreateFolderCommand : IRequest<FolderDto>
{
    public string Name { get; set; } = null!;
    public FolderType FolderType { get; set; }
    public List<CreateDocumentDto> Documents { get; set; } = null!;
    public Guid UserId { get; set; }
}