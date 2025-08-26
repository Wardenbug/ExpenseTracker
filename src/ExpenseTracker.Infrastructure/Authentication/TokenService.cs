using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class TokenService(
     ApplicationIdentityDbContext identityDbContext,
     IJwtService jwtService,
     IUserRepository userRepository,
     IOptions<JwtAuthOptions> options) : ITokenService
{
    private readonly JwtAuthOptions _jwtAuthOptions = options.Value;

    public async Task<Result<AccessTokenDto>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {

        var token = await identityDbContext.RefreshTokens
            .Where(token => token.Token == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return Result.Failure<AccessTokenDto>(
                new ApplicationError("Token.Get", "Token doesn't exist"));
        }

        if (token.ExpiresAtUtc < DateTime.UtcNow)
        {
            return Result.Failure<AccessTokenDto>(
                   new ApplicationError("Token.Get", "Token doesn't exist"));
        }

        var user = await userRepository.FindByIdentityIdAsync(token.UserId, cancellationToken);

        if(user is null)
        {
            return Result.Failure<AccessTokenDto>(
                  new ApplicationError("Token.Get", "Token doesn't exist"));
        }
        var accessToken = jwtService.CreateToken(new TokenRequest(user.Id));

        token.ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationInDays);
        token.Token = accessToken.RefreshToken;

        await identityDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(accessToken);
    }

    public async Task<Result> RevokeRefreshTokenAsync(
        string userId,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var token = await identityDbContext.RefreshTokens
            .Where(token => token.Token == refreshToken && token.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return Result.Failure(
                new ApplicationError("Token.Revoke", "Provided token doesn't exist"));
        }

        identityDbContext.Remove(token);

        await identityDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> SaveRefreshTokenAsync(
        string userId,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var refreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = refreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationInDays)
        };

        identityDbContext.RefreshTokens.Add(refreshTokenEntity);

        await identityDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
