using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Users;

public static class UserErrors
{
    public static readonly ApplicationError NotFound = new("User.Found", "User was not found");

    public static readonly ApplicationError InvalidCredentials = new(
       "User.InvalidCredentials",
       "The provided credentials were invalid");
}
