using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Constants;
using Domain.Exceptions;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SDIA.Security.TokenValidators;

public class GoogleTokenValidator : ISecurityTokenValidator
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }

    public bool CanReadToken(string securityToken) => true;

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
        [UnscopedRef] out SecurityToken validatedToken)
    {
        validatedToken = null;
        try
        {
            var payload = GoogleJsonWebSignature
                .ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { ClientId }
                }).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, payload.Name),
                new Claim(ClaimTypes.Name, payload.Name),
                new Claim(ClaimTypes.Email, payload.Email),
                new Claim(ClaimTypes.Sid, payload.Subject),
            };


            validatedToken = new JwtSecurityToken()
            {
                Payload =
                {
                    { "sub", payload.Subject },
                    { "email", payload.Email },
                    { "name", payload.Name },
                    { "iat", payload.ExpirationTimeSeconds },
                    { "exp", payload.ExpirationTimeSeconds }
                }
            };
            var principle = new ClaimsPrincipal();
            principle.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            return principle;
        }
        catch (InvalidJwtException e)
        {
            throw new NotAuthenticatedException(I18N.NotAuthenticated);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public bool CanValidateToken { get; }
    public int MaximumTokenSizeInBytes { get; set; }
}