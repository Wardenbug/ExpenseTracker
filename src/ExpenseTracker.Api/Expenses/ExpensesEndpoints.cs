using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Expenses;
using ExpenseTracker.Application.Expenses.CreateExpense;
using ExpenseTracker.Application.Expenses.DeleteExpense;
using ExpenseTracker.Application.Expenses.GetExpense;
using ExpenseTracker.Application.Expenses.GetExpenses;
using ExpenseTracker.Application.Expenses.UpdateExpense;
using ExpenseTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Expenses;

public static class ExpensesEndpoints
{
    public static IEndpointRouteBuilder MapExpensesEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("expenses")
                      .RequireAuthorization();

        group.MapPost("", CreateExpensesAsync);
        group.MapDelete("{id:guid}", DeleteExpenseById);
        group.MapPut("{id:guid}", UpdateExpenseById);
        group.MapGet("", GetExpenses);
        group.MapGet("{id:guid}", GetExpenseById)
            .WithName("GetExpenseById");

        return builder;
    }

    private static async Task<IResult> UpdateExpenseById(
        Guid id,
        UpdateExpenseRequest request,
        ICommandHandler<UpdateExpenseCommand, Result<ExpenseResponse>> handler,
        CancellationToken cancellationToken)
    {
        var command = new UpdateExpenseCommand(
            id,
            request.Name,
            request.CategoryType,
            request.Amount);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure && result.Errors?.Count > 0)
        {
            return Results.ValidationProblem(result.Errors!.ToValidationErrors());
        }

        if (result.IsFailure && result.Error is not null)
        {
            return Results.Problem(
                title: "Expense doesn't exist",
                detail: result.Error?.Message ?? "Unknown error",
                statusCode: StatusCodes.Status404NotFound);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> DeleteExpenseById(
        Guid id,
        ICommandHandler<DeleteExpenseCommand, Result> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteExpenseCommand(id);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {

            return Results.Problem(
                  title: "Expense doesn't exist",
                  detail: result.Error?.Message ?? "Unknown error",
                  statusCode: StatusCodes.Status404NotFound);
        }

        return Results.NoContent();
    }

    private static async Task<IResult> GetExpenseById(
        Guid id,
        IQueryHandler<GetExpenseQuery, Result<ExpenseResponse>> queryHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetExpenseQuery(id);

        var result = await queryHandler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            return Results.Problem(
                  title: "Expense doesn't exist",
                  detail: result.Error?.Message ?? "Unknown error",
                  statusCode: StatusCodes.Status404NotFound);
        }

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetExpenses(
        [AsParameters] GetExpensesParameters parameters,
        IQueryHandler<GetExpensesQuery, Result<PaginationResult<ExpenseResponse>>> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetExpensesQuery(parameters.Page, parameters.PageSize);

        var result = await handler.HandleAsync(query, cancellationToken);

        return Results.Ok(result.Value);
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
