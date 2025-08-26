using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Tokens.RevokeToken;

internal sealed class RevokeTokenCommandHandler(
    IUserService userService,
    IUserRepository userRepository,
    ITokenService tokenService)
    : ICommandHandler<RevokeTokenCommand, Result>
{
    public async Task<Result> HandleAsync(
        RevokeTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var userId = userService.UserId;

        var user = await userRepository.FindByIdAsync(userId, cancellationToken);

        if(user is null)
        {
            return Result.Failure(
                            new ApplicationError("Token.Revoke", "Failed to revoke refresh token"));
        }

        var result = await tokenService.RevokeRefreshTokenAsync(
            user.IdentityId, command.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(
                new ApplicationError("Token.Revoke", "Failed to revoke refresh token"));
        }

        return Result.Ok();
    }
}
