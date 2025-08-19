namespace ExpenseTracker.Api.Users;

public sealed record CreateUserRequest(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword);
