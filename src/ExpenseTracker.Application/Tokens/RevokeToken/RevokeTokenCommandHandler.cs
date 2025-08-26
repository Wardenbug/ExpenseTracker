using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Application.Tokens.RevokeToken;

internal sealed class RevokeTokenCommandHandler(
    IUserService userService,
    ITokenService tokenService)
    : ICommandHandler<RevokeTokenCommand, Result>
{
    public async Task<Result> HandleAsync(
        RevokeTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var userId = userService.UserId;

        var result = await tokenService.RevokeRefreshTokenAsync(userId, command.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(
                new ApplicationError("Token.Revoke", "Failed to revoke refresh token"));
        }

        return Result.Ok();
    }
}
