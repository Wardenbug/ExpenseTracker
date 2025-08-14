using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed class CreateExpenseCommandHandler(
        IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository
    ) : ICommandHandler<CreateExpenseCommand, int>
{
    public async Task<int> HandleAsync(
        CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var expense = Expense.Create(
            command.UserId,
            command.Name,
            command.Category,
            command.Amount,
            DateTime.UtcNow);

        expenseRepository.AddAsync(expense);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
