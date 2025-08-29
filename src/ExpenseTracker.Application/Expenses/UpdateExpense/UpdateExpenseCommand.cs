using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses.UpdateExpense;

public sealed record UpdateExpenseCommand(
    Guid Id,
    string Name,
    CategoryType CategoryType,
    decimal Amount) : ICommand;
    