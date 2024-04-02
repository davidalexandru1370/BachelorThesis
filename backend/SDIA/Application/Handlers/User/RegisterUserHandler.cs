using Application.Commands.User;
using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.User;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IJwtUtilities _jwtUtilities;
    
    public RegisterUserHandler(ISdiaDbContext dbContext, IJwtUtilities jwtUtilities)
    {
        _dbContext = dbContext;
        _jwtUtilities = jwtUtilities;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is not null)
        {
            throw new DuplicateEntryException(I18N.AccountAlreadyExists);
        }

        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var addedUser = await _dbContext.Users.AddAsync(request.Adapt<Domain.Entities.User>(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var token = _jwtUtilities.GenerateJwtTokenForUser(addedUser.Entity);

        return token;
    }
}