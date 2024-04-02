using Application.Commands.User;
using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.User;

public class RegisterUserWithGoogleHandler : IRequestHandler<RegisterUserWithGoogleCommand, string>
{
    private readonly ISdiaDbContext _dbContext;
    private readonly IJwtUtilities _jwtUtilities;

    public RegisterUserWithGoogleHandler(ISdiaDbContext dbContext, IJwtUtilities jwtUtilities)
    {
        _dbContext = dbContext;
        _jwtUtilities = jwtUtilities;
    }

    public async Task<string> Handle(RegisterUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var foundUser = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Sid == request.Sid);

        if (foundUser is not null)
        {
            if (foundUser.Email != request.Email ||
                foundUser.AuthenticationType != AuthenticationType.Google)
            {
                throw new BadRequestException(I18N.AccountAlreadyExists);
            }
        }
        else
        {
            string password = BCrypt.Net.BCrypt.HashPassword("");

            var user = new Domain.Entities.User
            {
                Email = request.Email,
                Password = password,
                AuthenticationType = AuthenticationType.Google,
                Role = Role.User,
                Sid = request.Sid,
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            foundUser = user; // This is done to avoid another query to the database
        }

        var token = _jwtUtilities.GenerateJwtTokenForUser(foundUser);

        return token;
    }
}