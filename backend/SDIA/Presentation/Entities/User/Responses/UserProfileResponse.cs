using Domain.Constants;

namespace SDIA.Entities.User.Responses;

public record UserProfileResponse
{
    public String Email { get; set; }
    public Role Role { get; set; }
    public AuthenticationType AuthenticationType { get; set; }
}