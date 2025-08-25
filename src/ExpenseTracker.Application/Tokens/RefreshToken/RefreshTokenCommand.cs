using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Tokens.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand;
