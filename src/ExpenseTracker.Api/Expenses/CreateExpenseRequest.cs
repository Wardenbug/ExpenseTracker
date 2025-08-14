namespace ExpenseTracker.Api.Expenses;
public record CreateExpenseRequest(Guid UserId,
    string Name,
    CategoryType Category,
    decimal Amount);

