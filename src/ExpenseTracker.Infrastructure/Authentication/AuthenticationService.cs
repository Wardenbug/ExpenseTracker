using ExpenseTracker.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class AuthenticationService(
    UserManager<IdentityUser> userManager
    ) : IAuthenticationService
{
    public async Task<string> RegisterUserAsync(
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
            throw new InvalidOperationException("Unable to create the user");
        }

        return identityUser.Id;
    }
}
