using Application.Commands.User;
using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using MediatR;

namespace Application.Handlers.User;

public class RegisterUserWithGoogleHandler : IRequestHandler<RegisterUserWithGoogleCommand>
{
    private readonly ISdiaDbContext _dbContext;

    public RegisterUserWithGoogleHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(RegisterUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var foundUser = _dbContext.Users.FirstOrDefault(u => u.Sid == request.Sid);
        
        if (foundUser is not null)
        {
            if (foundUser.Email != request.Email ||
                foundUser.AuthorizationType != AuthorizationType.Google)
            {
                throw new BadRequestException(I18N.DuplicateEntry);
            }

            return;
        }

        string password = BCrypt.Net.BCrypt.HashPassword("");

        var user = new Domain.Entities.User
        {
            Email = request.Email,
            Password = password,
            AuthorizationType = AuthorizationType.Google,
            Sid = request.Sid,
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}