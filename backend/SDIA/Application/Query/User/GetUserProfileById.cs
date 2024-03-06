using Application.DTOs;
using MediatR;

namespace Application.Query.User;

public record GetUserProfileById : IRequest<UserDto>
{
    public Guid UserId { get; init; }
}