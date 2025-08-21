using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Users.LoginUser;

public sealed record LoginUserCommand(string UserName, string Password) : ICommand;
