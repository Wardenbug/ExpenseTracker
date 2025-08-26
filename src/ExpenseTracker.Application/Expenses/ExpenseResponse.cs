using ExpenseTracker.Domain.Expenses;

namespace ExpenseTracker.Application.Expenses;

public sealed record ExpenseResponse(
    Guid Id,
    string Name,
    decimal Amount,
    Category Category);
