using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id,
        string userName,
        string email) : base(id)
    {
        UserName = userName;
        Email = email;
    }

    private User()
    {

    }

    public string UserName { get; private set; }
    public string Email { get; private set; }

    public static User Create(string userName, string email)
    {
        return new User(Guid.NewGuid(), userName, email);
    }
}
