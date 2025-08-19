using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword) : ICommand;
