using FluentValidation;

namespace ExpenseTracker.Application.Expenses.UpdateExpense;

internal sealed class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(command =>
         command.Name).NotEmpty();

        RuleFor(command =>
          command.CategoryType).NotEmpty();

        RuleFor(command =>
          command.Amount).GreaterThan(0);
    }
}
