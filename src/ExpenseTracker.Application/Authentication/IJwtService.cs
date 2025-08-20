namespace ExpenseTracker.Application.Authentication;

public interface IJwtService
{
    AccessTokenDto CreateToken(TokenRequest tokenRequest);
}
