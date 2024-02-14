using System.Security.Claims;
using Domain.Exceptions;

namespace SDIA.Security;

public static class IdentityExtensions
{
    public static Guid GetId(this ClaimsPrincipal claims)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null || String.IsNullOrWhiteSpace(userId))
        {
            throw new NotAuthenticatedException("Not Authenticated");
        }

        return Guid.Parse(userId);
    }
    
    public static string GetEmail(this ClaimsPrincipal claims)
    {
        
        var email =  claims.FindFirstValue(ClaimTypes.Email);
        
        if(email is null || String.IsNullOrWhiteSpace(email))
        {
            throw new NotAuthenticatedException("Not Authenticated");
        }
        
        return email;
    }
    
    public static string GetSid(this ClaimsPrincipal claims)
    {
        var sid = claims.FindFirstValue(ClaimTypes.Sid);
        
        if(sid is null || String.IsNullOrWhiteSpace(sid))
        {
            throw new NotAuthenticatedException("Not Authenticated");
        }
        
        return sid;
    }
}