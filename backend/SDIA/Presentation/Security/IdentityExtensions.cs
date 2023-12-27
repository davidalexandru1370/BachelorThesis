using System.Security.Claims;
using Domain.Exceptions;

namespace SDIA.Security;

public static class IdentityExtensions
{
    public static Guid GetId(this ClaimsPrincipal claims)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId is null)
        {
            throw new NotAuthenticatedException("Not Authenticated");
        }
        
        return Guid.Parse(userId);
    }
}