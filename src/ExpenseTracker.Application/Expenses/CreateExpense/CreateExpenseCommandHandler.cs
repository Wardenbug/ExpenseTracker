using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed class CreateExpenseCommandHandler(
        IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository
    ) : ICommandHandler<CreateExpenseCommand, Expense>
{
    public async Task<Expense> HandleAsync(
        CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var category = command.CategoryType switch
        {
            CategoryType.Groceries => Category.Groceries,
            CategoryType.Leisure => Category.Leisure,
            CategoryType.Electronics => Category.Electronics,
            CategoryType.Utilities => Category.Utilities,
            CategoryType.Clothing => Category.Clothing,
            CategoryType.Health => Category.Health,
            _ => Category.Others
        };

        var expense = Expense.Create(
            command.UserId,
            command.Name,
            category,
            command.Amount,
            DateTime.UtcNow);

        await expenseRepository.AddAsync(expense, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return expense;
    }
}
