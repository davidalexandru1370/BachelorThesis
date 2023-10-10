using MediatR;

namespace Application.Interfaces;

public interface ITransanctionalCommand<TCommand> : IRequest<TCommand>
where TCommand : new()
{
    
}