namespace ExpenseTracker.Application.Abstractions;

public interface IAuthenticationService
{
    Task<string> RegisterUserAsync(
        string email,
        string userName,
        string password,
        CancellationToken cancellationToken = default);
}
