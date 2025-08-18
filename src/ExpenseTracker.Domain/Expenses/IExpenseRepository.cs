namespace ExpenseTracker.Domain.Expenses;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken cancellationToken = default);
}
