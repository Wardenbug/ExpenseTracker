using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Api.Expenses;

public static class ExpensesEndpoints
{
    public static IEndpointRouteBuilder MapExpensesEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("expenses")
                      .RequireAuthorization();

        group.MapPost("", CreateExpensesAsync);
        group.MapGet("", GetExpenses);
        group.MapGet("{id}", GetExpenseById)
            .WithName("GetExpenseById");

        return builder;
    }

    private static Task GetExpenseById(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetExpenses(HttpContext context)
    {
        throw new NotImplementedException();
    }

    public static async Task<IResult> CreateExpensesAsync(
        CreateExpenseRequest request,
        ICommandHandler<CreateExpenseCommand, Result<ExpenseResponse>> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateExpenseCommand(
            request.Name,
            request.CategoryType,
            request.Amount);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.ValidationProblem(result.Errors!.ToValidationErrors());
        }

        return Results.CreatedAtRoute("GetExpenseById", new { id = result.Value!.Id }, result.Value);
    }
}
