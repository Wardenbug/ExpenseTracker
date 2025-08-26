using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;
using FluentValidation;
using FluentValidation.Results;

namespace ExpenseTracker.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IJwtService jwtService,
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    ITokenService tokenService,
    IValidator<LoginUserCommand> validator
    ) : ICommandHandler<LoginUserCommand, Result<AccessTokenDto>>
{
    public async Task<Result<AccessTokenDto>> HandleAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<AccessTokenDto>(validationResult.Errors.ToApplicationErrors());
        }

        var loginResult = await authenticationService.LoginUserAsync(
            command.UserName,
            command.Password,
            cancellationToken);


        if (loginResult.IsFailure)
        {
            return Result.Failure<AccessTokenDto>(
                new ApplicationError("User.Login", "User doesn't exist"));
        }

        var user = await userRepository.FindByIdentityIdAsync(loginResult.Value!, cancellationToken);

        if (user is null)
        {
            return Result.Failure<AccessTokenDto>(
           new ApplicationError("User.Login", "User doesn't exist"));
        }

        var token = jwtService.CreateToken(new TokenRequest(user.Id));

        await tokenService.SaveRefreshTokenAsync(
            loginResult.Value!,
            token.RefreshToken,
            cancellationToken);

        return Result.Ok(token);
    }
}
