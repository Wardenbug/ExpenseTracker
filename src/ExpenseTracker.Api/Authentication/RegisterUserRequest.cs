namespace ExpenseTracker.Api.Authentication;

public sealed record RegisterUserRequest(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword);
