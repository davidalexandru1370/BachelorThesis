using MediatR;

namespace Application.Commands.Folder;

public record DeleteFolderById(Guid Id) : IRequest
{
    
}