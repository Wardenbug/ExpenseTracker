using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Infrastructure.Authentication;

public sealed class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAtUtc { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public IdentityUser User { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
