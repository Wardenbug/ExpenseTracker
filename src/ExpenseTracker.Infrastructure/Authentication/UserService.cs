using ExpenseTracker.Application.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ExpenseTracker.Infrastructure.Authentication;

internal sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("User id is unavailable");
}
