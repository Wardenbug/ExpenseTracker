using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.GetExpenses;

internal sealed class GetExpensesQueryHandler(
    IExpenseRepository expenseRepository,
    IUserService userService) :
    IQueryHandler<GetExpensesQuery, Result<PaginationResult<ExpenseResponse>>>
{
    public async Task<Result<PaginationResult<ExpenseResponse>>> HandleAsync(
        GetExpensesQuery command,
        CancellationToken cancellationToken = default)
    {
        var userId = userService.UserId;

        var paginatedExpenses = await expenseRepository.GetExpenses(
            userId,
            command.Page,
            command.PageSize,
            cancellationToken);

        var expenseResponses = paginatedExpenses.Items.Select(expense =>
           new ExpenseResponse(expense.Id, expense.Name, expense.Amount, expense.Category));

        var paginatedResponse = new PaginationResult<ExpenseResponse>
        {
            Items = expenseResponses.ToList(),
            Page = paginatedExpenses.Page,
            PageSize = paginatedExpenses.PageSize,
            TotalCount = paginatedExpenses.TotalCount
        };

        return Result.Ok(paginatedResponse);
    }
}
