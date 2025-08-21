using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Users.LoginUser;
using ExpenseTracker.Application.Users.RegisterUser;

namespace ExpenseTracker.Api.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("users", RegisterUser);
        routeBuilder.MapPost("users/login", LoginUser);

        return routeBuilder;
    }

    public static async Task<IResult> RegisterUser(
        CreateUserRequest request,
        ICommandHandler<RegisterUserCommand, AccessTokenDto> handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.UserName,
            request.Email,
            request.Password,
            request.ConfirmPassword
            );

        var newUser = await handler.HandleAsync(command, cancellationToken);

        return TypedResults.Ok(newUser);
    }

    public static async Task<IResult> LoginUser(
        LoginUserRequest request,
        ICommandHandler<LoginUserCommand, AccessTokenDto> handler,
        CancellationToken cancellationToken
        )
    {
        var command = new LoginUserCommand(
            request.UserName,
            request.Password);

        var token = await handler.HandleAsync(command, cancellationToken);

        return Results.Ok(token);
    }
}
