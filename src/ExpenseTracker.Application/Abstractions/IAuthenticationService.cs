using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Application.Abstractions;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterUserAsync(
        string email,
        string userName,
        string password,
        CancellationToken cancellationToken = default);

    Task<Result<string>> LoginUserAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default);
}
