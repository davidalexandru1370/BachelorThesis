using Application.DTOs;
using MediatR;

namespace Application.Query.Folder;

public record GetFolderByIdQuery(Guid FolderId) : IRequest<FolderDto>
{
}