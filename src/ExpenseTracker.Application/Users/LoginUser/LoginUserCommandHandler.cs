using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;

namespace ExpenseTracker.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IJwtService jwtService,
    IAuthenticationService authenticationService
    ) : ICommandHandler<LoginUserCommand, AccessTokenDto>
{
    public async Task<AccessTokenDto> HandleAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var userId = await authenticationService.LoginUserAsync(
            command.UserName,
            command.Password,
            cancellationToken);

        var token = jwtService.CreateToken(new TokenRequest(userId));

        return token;
    }
}
