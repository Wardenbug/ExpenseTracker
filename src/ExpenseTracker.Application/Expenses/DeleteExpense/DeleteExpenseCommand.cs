using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Expenses.DeleteExpense;

public sealed record DeleteExpenseCommand(Guid ExpenseId) : ICommand;
