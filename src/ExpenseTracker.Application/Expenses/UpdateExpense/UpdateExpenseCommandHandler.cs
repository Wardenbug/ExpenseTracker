using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using FluentValidation;

namespace ExpenseTracker.Application.Expenses.UpdateExpense;

internal sealed class UpdateExpenseCommandHandler(
    IExpenseRepository expenseRepository,
    IValidator<UpdateExpenseCommand> validator,
    IUnitOfWork unitOfWork,
    IUserService userService) : ICommandHandler<UpdateExpenseCommand, Result<ExpenseResponse>>
{
    public async Task<Result<ExpenseResponse>> HandleAsync(
        UpdateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<ExpenseResponse>(validationResult.Errors.ToApplicationErrors());
        }

        var expense = await expenseRepository.GetByIdAsync(command.Id, cancellationToken);

        if (expense is null)
        {
            return Result.Failure<ExpenseResponse>(
                new ApplicationError("Expense.Update", "Expense doesn't exist"));
        }

        var userId = userService.UserId;

        if (expense.UserId != userId)
        {
            return Result.Failure<ExpenseResponse>(
               new ApplicationError("Expense.Update", "Expense doesn't exist"));
        }

        var category = command.CategoryType.ToCategory();

        expense.Update(
            command.Name,
            category,
            command.Amount
            );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(new ExpenseResponse(
            expense.Id,
            expense.Name,
            expense.Amount,
            expense.Category
            ));
    }
}
