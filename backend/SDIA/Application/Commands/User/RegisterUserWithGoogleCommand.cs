using MediatR;

namespace Application.Commands.User;

public record RegisterUserWithGoogleCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string Sid { get; set; }
}