using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Api.Expenses;

internal sealed record UpdateExpenseRequest(string Name,
    CategoryType CategoryType,
    decimal Amount);
