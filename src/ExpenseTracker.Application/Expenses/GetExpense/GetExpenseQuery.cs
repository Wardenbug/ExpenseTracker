using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Expenses.GetExpense;

public sealed record GetExpenseQuery(Guid ExpenseId) : IQuery;
