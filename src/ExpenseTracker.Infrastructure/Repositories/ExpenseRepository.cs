using ExpenseTracker.Domain.Abstractions;
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

    public void Delete(Expense expense)
    {
        applicationDbContext.Remove(expense);
    }

    public async Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var expense = await applicationDbContext.Set<Expense>()
            .FirstOrDefaultAsync(expense => expense.Id == id, cancellationToken);

        return expense;
    }

    public async Task<PaginationResult<Expense>> GetExpenses(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var expensesQuery = applicationDbContext.Set<Expense>()
            .Where(expense => expense.UserId == userId);

        var totalCount = await expensesQuery.CountAsync(cancellationToken);

        var expenses = await expensesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var paginatedResult = new PaginationResult<Expense>
        {
            Items = expenses,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return paginatedResult;
    }
}
