using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.DeleteExpense;

internal sealed class DeleteExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork,
    IUserService userService) : ICommandHandler<DeleteExpenseCommand, Result>
{
    public async Task<Result> HandleAsync(
        DeleteExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var expense = await expenseRepository.GetByIdAsync(command.ExpenseId, cancellationToken);

        if (expense is null)
        {
            return Result.Failure(
                new ApplicationError("Expense.Delete", "Expense doesn't exist"));
        }

        var userId = userService.UserId;

        if (expense.UserId != userId)
        {
            return Result.Failure(
               new ApplicationError("Expense.Delete", "Expense doesn't exist"));
        }


        expenseRepository.Delete(expense);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
