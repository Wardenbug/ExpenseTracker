namespace ExpenseTracker.Api.Expenses;

internal sealed record GetExpensesParameters(int Page = 1, int PageSize = 10);
