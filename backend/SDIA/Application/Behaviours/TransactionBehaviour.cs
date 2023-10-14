using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest: ITransanctionalCommand
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public TransactionBehaviour(ISdiaDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.BeginTransactionAsync(cancellationToken);
    
            var response = await next();

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}