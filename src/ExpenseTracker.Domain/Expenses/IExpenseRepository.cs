namespace ExpenseTracker.Domain.Expenses;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken cancellationToken = default);

    Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Delete(Expense expense);
}
