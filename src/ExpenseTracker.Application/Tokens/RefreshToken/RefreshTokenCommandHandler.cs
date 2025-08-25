using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Application.Tokens.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    ITokenService tokenService)
    : ICommandHandler<RefreshTokenCommand, Result<AccessTokenDto>>
{
    public async Task<Result<AccessTokenDto>> HandleAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await tokenService.RefreshTokenAsync(command.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenDto>(result.Error!);
        }

        return Result.Ok(result.Value!);
    }
}
