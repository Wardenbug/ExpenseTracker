using ExpenseTracker.Application.Authentication;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new InvalidOperationException("User context is unavailable");
}
