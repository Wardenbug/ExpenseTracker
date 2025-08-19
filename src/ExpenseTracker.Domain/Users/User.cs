using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id,
        string userName,
        string email,
        string identityId) : base(id)
    {
        UserName = userName;
        Email = email;
        IdentityId = identityId;
    }

    private User()
    {

    }

    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string IdentityId { get; private set; } = string.Empty;

    public static User Create(string userName, string email, string identityId)
    {
        return new User(Guid.NewGuid(), userName, email, identityId);
    }
}
