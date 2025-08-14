namespace ExpenseTracker.Domain.Expenses;

public interface IExpenseRepository
{
    void AddAsync(Expense expense);
}
