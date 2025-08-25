using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Tokens.RefreshToken;
using ExpenseTracker.Application.Users.LoginUser;
using ExpenseTracker.Application.Users.RegisterUser;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Api.Authentication;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("auth/register", Register);
        routeBuilder.MapPost("auth/login", Login);
        routeBuilder.MapPost("auth/refresh", Refresh);

        return routeBuilder;
    }

    private static async Task<IResult> Refresh(
        RefreshTokenRequest request,
        ICommandHandler<RefreshTokenCommand, Result<AccessTokenDto>> handler,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> Register(
        RegisterUserRequest request,
        ICommandHandler<RegisterUserCommand, Result<Guid>> handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.UserName,
            request.Email,
            request.Password,
            request.ConfirmPassword
            );

        var token = await handler.HandleAsync(command, cancellationToken);

        if (token.IsFailure)
        {
            return Results.ValidationProblem(token.Errors!.ToValidationErrors());
        }

        return TypedResults.Ok(token.Value);
    }

    public static async Task<IResult> Login(
        LoginUserRequest request,
        ICommandHandler<LoginUserCommand, Result<AccessTokenDto>> handler,
        CancellationToken cancellationToken
        )
    {
        var command = new LoginUserCommand(
            request.UserName,
            request.Password);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(result.Value);
    }
}
