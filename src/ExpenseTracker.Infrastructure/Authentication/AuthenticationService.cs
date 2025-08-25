using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class AuthenticationService(
    UserManager<IdentityUser> userManager
    ) : IAuthenticationService
{
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
}
