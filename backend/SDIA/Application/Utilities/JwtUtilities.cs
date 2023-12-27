using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Utilities;

public class JwtUtilities : IJwtUtilities
{
    private readonly IConfiguration _appSettings;
    
    public JwtUtilities(IConfiguration appSettings)
    {
        _appSettings = appSettings;
    }
    
    public string GenerateJwtTokenForUser(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_appSettings["JwtSettings:Key"]);
        TimeSpan tokenLifetime = TimeSpan.FromHours(_appSettings.GetValue<int>("JwtSettings:TokenLifetimeInHours"));
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.Add(tokenLifetime),
            Audience = _appSettings["JwtSettings:Audience"],
            Issuer = _appSettings["JwtSettings:Issuer"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }

    public Guid? ValidateToken(string token)
    {
        if (token is null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_appSettings["JwtSettings:Key"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwt = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwt.Claims.First(x => x.Type == "Id").Value);
            return userId;
        }
        catch (SecurityTokenValidationException securityTokenValidationException)
        {
        }

        return null;
    }
}