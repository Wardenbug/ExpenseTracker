namespace ExpenseTracker.Application.Authentication;

public sealed record AccessTokenDto(string AccessToken, string RefreshToken);
