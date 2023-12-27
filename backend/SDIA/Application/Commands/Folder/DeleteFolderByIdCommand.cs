using Application.Interfaces;
using MediatR;

namespace Application.Commands.Folder;

public record DeleteFolderByIdCommand(Guid Id, Guid UserId) : IRequest
{
}