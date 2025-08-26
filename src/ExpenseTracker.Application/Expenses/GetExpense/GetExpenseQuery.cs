using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Expenses.GetExpense;

public sealed record GetExpenseQuery(string ExpenseId) : IQuery;
