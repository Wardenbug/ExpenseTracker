using ExpenseTracker.Domain.Users;
using ExpenseTracker.Infrastructure.Data;

namespace ExpenseTracker.Infrastructure.Repositories;

internal sealed class UserRepository(
    ApplicationDbContext applicationDbContext
    ) : IUserRepository
{
    public void Add(User user)
    {
        applicationDbContext.Add(user);
    }
}
