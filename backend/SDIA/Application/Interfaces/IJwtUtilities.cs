using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtUtilities
{
    public string GenerateJwtTokenForUser(User user);
    public Guid? ValidateToken(string token);
}