using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Domain.Expenses;

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
        ICommandHandler<CreateExpenseCommand, Expense> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateExpenseCommand(
            request.UserId,
            request.Name,
            request.CategoryType,
            request.Amount);

        var expense = await handler.HandleAsync(command, cancellationToken);

        return Results.Ok(expense);
    }
}
