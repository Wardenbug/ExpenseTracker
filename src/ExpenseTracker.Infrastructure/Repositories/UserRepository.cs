using ExpenseTracker.Domain.Users;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories;

internal sealed class UserRepository(
    ApplicationDbContext applicationDbContext
    ) : IUserRepository
{
    private DbSet<User> users => applicationDbContext.Set<User>();
    public void Add(User user)
    {
        applicationDbContext.Add(user);
    }

    public async Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        return user;
    }

    public async Task<User?> FindByIdentityIdAsync(
        string identityId,
        CancellationToken cancellationToken = default)
    {
        var user = await users
             .Where(user => user.IdentityId == identityId)
             .FirstOrDefaultAsync(cancellationToken);

        return user;
    }
}
