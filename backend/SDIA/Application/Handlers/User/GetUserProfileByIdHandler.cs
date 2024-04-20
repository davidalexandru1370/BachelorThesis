using Application.DTOs;
using Application.Interfaces;
using Application.Query.User;
using Domain.Constants;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.User;

public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileByIdQuery, UserDto>
{
    private readonly ISdiaDbContext _dbContext;

    public GetUserProfileByIdHandler(ISdiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(I18N.DoesNotExist);
        }

        return user.Adapt<UserDto>();
    }
}