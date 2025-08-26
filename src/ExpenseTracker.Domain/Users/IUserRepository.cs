namespace ExpenseTracker.Domain.Users;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> FindByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);

    Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
