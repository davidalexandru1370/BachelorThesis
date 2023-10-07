using MediatR;

namespace Application.Commands.Folder;

public record DeleteFolderByIdCommand(Guid Id) : IRequest
{
    
}