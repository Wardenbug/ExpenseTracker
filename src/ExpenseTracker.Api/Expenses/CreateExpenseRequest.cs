using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Api.Expenses;
public record CreateExpenseRequest(Guid UserId,
    string Name,
    CategoryType CategoryType,
    decimal Amount);