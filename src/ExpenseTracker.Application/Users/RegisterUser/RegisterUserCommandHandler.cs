using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;
using FluentValidation;
using FluentValidation.Results;

namespace ExpenseTracker.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthenticationService authenticationService,
    IValidator<RegisterUserCommand> validator,
    IJwtService jwtService
    ) : ICommandHandler<RegisterUserCommand, Result<AccessTokenDto>>
{
    public async Task<Result<AccessTokenDto>> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<AccessTokenDto>(validationResult.Errors.ToApplicationErrors());
        }

        var result = await authenticationService.RegisterUserAsync(
            command.Email,
            command.UserName,
            command.Password,
            cancellationToken
            );

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenDto>(result.Errors!);
        }

        var user = User.Create(command.UserName, command.Email, result.Value!);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = jwtService.CreateToken(new TokenRequest(user.IdentityId));

        return Result.Ok(accessToken);
    }
}
