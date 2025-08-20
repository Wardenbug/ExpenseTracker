using ExpenseTracker.Application.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class JwtService(IOptions<JwtAuthOptions> jwtOptions) : IJwtService
{
    private readonly JwtAuthOptions _jwtAuthOptions = jwtOptions.Value;

    public AccessTokenDto CreateToken(TokenRequest tokenRequest)
    {
        return new AccessTokenDto(GenerateAccessToken(tokenRequest), GenerateRefreshToken());
    }

    private string GenerateAccessToken(TokenRequest tokenRequest)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, tokenRequest.UserId),
            ];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtAuthOptions.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtAuthOptions.Issuer,
            Audience = _jwtAuthOptions.Audience
        };

        var handler = new JsonWebTokenHandler();

        var accesToken = handler.CreateToken(tokenDescriptor);

        return accesToken;
    }

    private static string GenerateRefreshToken()
    {
        return string.Empty;
    }
}
