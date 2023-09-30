namespace Application.Commands.UserCommands;

public record LoginUserCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}