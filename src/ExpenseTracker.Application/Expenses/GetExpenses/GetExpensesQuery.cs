using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Expenses.GetExpenses;

public sealed record GetExpensesQuery(int Page, int PageSize) : IQuery;
