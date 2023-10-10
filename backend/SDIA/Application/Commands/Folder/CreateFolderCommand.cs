using Application.DTOs;
using Domain.Constants;
using MediatR;

namespace Application.Commands.Folder;

public class CreateFolderCommand : IRequest<FolderDto>
{
    public string Name { get; set; } = null!;
    public FolderType FolderType { get; set; }
    public List<CreateDocumentDto> Documents { get; set; } = null!;
    public Guid UserId { get; set; }
}