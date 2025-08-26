using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Authentication;
using ExpenseTracker.Application.Extensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using FluentValidation;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

public sealed class CreateExpenseCommandHandler(
        IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository,
        IUserService userService,
        IValidator<CreateExpenseCommand> validator
    ) : ICommandHandler<CreateExpenseCommand, Result<ExpenseResponse>>
{
    public async Task<Result<ExpenseResponse>> HandleAsync(
        CreateExpenseCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<ExpenseResponse>(validationResult.Errors.ToApplicationErrors());
        }

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
            userService.UserId,
            command.Name,
            category,
            command.Amount,
            DateTime.UtcNow);

        await expenseRepository.AddAsync(expense, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(
            new ExpenseResponse(expense.Id, expense.Name, expense.Amount, category));
    }
}
