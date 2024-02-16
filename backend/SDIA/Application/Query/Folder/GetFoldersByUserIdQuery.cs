using Application.DTOs;
using MediatR;

namespace Application.Query.Folder;

public record GetFoldersByUserIdQuery(Guid UserId, String Sid) : IRequest<List<FolderDto>>
{
    
}