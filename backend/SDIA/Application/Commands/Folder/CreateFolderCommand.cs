using Application.DTOs;
using Domain.Constants;
using MediatR;

namespace Application.Commands.Folder;

public class CreateFolderCommand : IRequest<FolderDto>
{
    public string StorageUrl { get; set; } = null!;
    public string Name { get; set; } = null!;
    public FolderType FolderType { get; set; }
    public List<DocumentDto> Documents { get; set; } = null!;
    public Guid UserId { get; set; }
}