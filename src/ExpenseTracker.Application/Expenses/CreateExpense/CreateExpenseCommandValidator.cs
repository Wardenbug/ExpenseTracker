using ExpenseTracker.Domain.Expenses;
using FluentValidation;

namespace ExpenseTracker.Application.Expenses.CreateExpense;

internal sealed class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(expense =>
          expense.Name).NotEmpty();

        RuleFor(expense =>
          expense.CategoryType).NotEmpty();

        RuleFor(expense =>
          expense.Amount).GreaterThan(0);
    }
}
