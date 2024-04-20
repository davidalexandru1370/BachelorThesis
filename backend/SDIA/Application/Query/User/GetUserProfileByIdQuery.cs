using Application.DTOs;
using MediatR;

namespace Application.Query.User;

public record GetUserProfileByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; init; }
}