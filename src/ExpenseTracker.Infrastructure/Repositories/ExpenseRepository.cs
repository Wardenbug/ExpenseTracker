using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories;

internal sealed class ExpenseRepository(
    ApplicationDbContext applicationDbContext
    ) : IExpenseRepository
{
    public async Task AddAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        await applicationDbContext.AddAsync(expense, cancellationToken);
    }

    public async Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var expense = await applicationDbContext.Set<Expense>()
            .FirstOrDefaultAsync(expense => expense.Id == id, cancellationToken);

        return expense;
    }
}
