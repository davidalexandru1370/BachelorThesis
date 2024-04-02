using Domain.Constants;

namespace Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
}