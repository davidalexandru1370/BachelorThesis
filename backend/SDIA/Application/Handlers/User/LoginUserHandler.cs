using Application.Commands.UserCommands;
using Application.DTOs;
using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.User;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserDto>
{
    private readonly ISdiaDbContext _dbContext;

    public LoginUserHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(I18N.EmailDoesNotExist);
        }

        if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password) is false)
        {
            throw new BadRequestException(I18N.InvalidEmailOrPassword);
        }

        return user.Adapt<UserDto>();
    }
}