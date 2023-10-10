using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransanctionalCommand<TResponse>
    where TResponse : new()
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly ILogger _logger;

    public TransactionBehaviour(ISdiaDbContext dbContext, IHttpContextAccessor httpContext, ILogger logger)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(string.Format("User {0} is requesting {1}", _httpContext.HttpContext.User.Identity.Name, request.GetType().Name));
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