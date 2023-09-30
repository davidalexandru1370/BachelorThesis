using Application.Commands.UserCommands;
using Application.DTOs;
using Application.Interfaces;
using Domain.Constants;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.User;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly ISdiaDbContext _dbContext;

    public RegisterUserHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is not null)
        {
            throw new NotFoundException(I18N.EmailDoesNotExist);
        }

        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var addedUser = await _dbContext.Users.AddAsync(request.Adapt<Domain.Entities.User>(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return addedUser.Entity.Adapt<UserDto>();
    }
}