using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Application.Abstractions;

public interface ITokenService
{
    Task<Result> SaveRefreshTokenAsync(
       string userId,
       string refreshToken,
       CancellationToken cancellationToken = default);

    Task<Result> RevokeRefreshTokenAsync(
       string userId,
       string refreshToken,
       CancellationToken cancellationToken = default);

    Task<Result<AccessTokenDto>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
