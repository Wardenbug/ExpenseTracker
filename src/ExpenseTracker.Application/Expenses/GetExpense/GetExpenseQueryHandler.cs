using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.GetExpense;

internal sealed class GetExpenseQueryHandler 
    : IQueryHandler<GetExpenseQuery, Result<Expense>>
{
    public Task<Result<Expense>> HandleAsync(
        GetExpenseQuery command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
