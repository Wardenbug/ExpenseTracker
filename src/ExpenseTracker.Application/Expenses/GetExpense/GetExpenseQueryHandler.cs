using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.GetExpense;

internal sealed class GetExpenseQueryHandler(
    IExpenseRepository expenseRepository,
    IUserService userService)
    : IQueryHandler<GetExpenseQuery, Result<ExpenseResponse>>
{
    public async Task<Result<ExpenseResponse>> HandleAsync(
        GetExpenseQuery command,
        CancellationToken cancellationToken = default)
    {
        var expense = await expenseRepository.GetByIdAsync(command.ExpenseId, cancellationToken);

        if (expense is null)
        {
            return Result.Failure<ExpenseResponse>(
                new ApplicationError("Expense.GetById", "Expense doesn't exist"));
        }

        var userId = userService.UserId;

        if (expense.UserId != userId)
        {
            return Result.Failure<ExpenseResponse>(
              new ApplicationError("Expense.GetById", "Expense doesn't exist"));
        }

        return Result.Ok(new ExpenseResponse(expense.Id,
            expense.Name,
            expense.Amount,
            expense.Category));
    }
}
