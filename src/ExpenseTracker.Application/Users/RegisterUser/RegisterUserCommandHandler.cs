using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthenticationService authenticationService
    ) : ICommandHandler<RegisterUserCommand, User>
{
    public async Task<User> HandleAsync(
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

        return user;
    }
}
