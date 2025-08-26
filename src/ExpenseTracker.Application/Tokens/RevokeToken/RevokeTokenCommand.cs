using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Tokens.RevokeToken;

public sealed record RevokeTokenCommand(string RefreshToken) : ICommand;
    