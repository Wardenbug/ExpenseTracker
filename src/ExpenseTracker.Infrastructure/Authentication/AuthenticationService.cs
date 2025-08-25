using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class AuthenticationService(
    UserManager<IdentityUser> userManager,
     ApplicationIdentityDbContext identityDbContext,
     IOptions<JwtAuthOptions> options
    ) : IAuthenticationService
{
    private readonly JwtAuthOptions _jwtAuthOptions = options.Value;

    public async Task<Result<string>> LoginUserAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFound);
        }

        var isCorrectPassword = await userManager.CheckPasswordAsync(user, password);

        if (!isCorrectPassword)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        return Result.Ok(user.Id);
    }

    public async Task<Result<string>> RegisterUserAsync(
        string email,
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        var identityUser = new IdentityUser
        {
            Email = email,
            UserName = userName,
        };

        IdentityResult result = await userManager.CreateAsync(identityUser, password);

        if (!result.Succeeded)
        {
            return Result.Failure<string>(
                result.Errors.Select(error =>
                    new ApplicationError(error.Code, error.Description)).ToList());
        }

        return Result.Ok(identityUser.Id);
    }

    public async Task<Result> SaveRefreshTokenAsync(
        string userId,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var refreshTokenEntity = new RefreshToken
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
