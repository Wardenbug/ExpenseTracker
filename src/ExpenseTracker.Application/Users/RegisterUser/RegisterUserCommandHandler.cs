using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthenticationService authenticationService,
    IJwtService jwtService
    ) : ICommandHandler<RegisterUserCommand, AccessTokenDto>
{
    public async Task<AccessTokenDto> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await authenticationService.RegisterUserAsync(
            command.Email,
            command.UserName,
            command.Password,
            cancellationToken
            );

        var user = User.Create(command.UserName, command.Email, result);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = jwtService.CreateToken(new TokenRequest(user.IdentityId));

        return accessToken;
    }
}
