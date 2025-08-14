using ExpenseTracker.Application.Expenses.CreateExpense;

namespace ExpenseTracker.Api.Expenses;

public static class ExpensesEndpoints
{
    public static IEndpointRouteBuilder MapExpensesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("expenses", CreateExpensesAsync);
        builder.MapGet("expenses", GetExpenses);
        builder.MapGet("expenses/{id}", GetExpenseById);

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
        CreateExpenseCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateExpenseCommand(
            request.UserId,
            request.Name,
            request.Category,
            request.Amount);

        var result = await handler.HandleAsync(command, cancellationToken);

        return Results.Ok();
    }
}
