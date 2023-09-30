using Application.DTOs;
using MediatR;

namespace Application.Commands.UserCommands;

public record RegisterUserCommand : IRequest<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}