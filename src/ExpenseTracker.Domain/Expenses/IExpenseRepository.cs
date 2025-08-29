using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken cancellationToken = default);

    Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginationResult<Expense>> GetExpenses(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    void Delete(Expense expense);
}
