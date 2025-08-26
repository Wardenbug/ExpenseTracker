using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Api.Expenses;
public record CreateExpenseRequest(string Name,
    CategoryType CategoryType,
    decimal Amount);