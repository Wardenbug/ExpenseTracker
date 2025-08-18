using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Infrastructure.Data;

namespace ExpenseTracker.Infrastructure.Repositories;

internal sealed class ExpenseRepository(
    ApplicationDbContext applicationDbContext) : IExpenseRepository
{
    public async Task AddAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        await applicationDbContext.AddAsync(expense, cancellationToken);
    }
}
