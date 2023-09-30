namespace Application.Commands.UserCommands;

public record RegisterUserCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}