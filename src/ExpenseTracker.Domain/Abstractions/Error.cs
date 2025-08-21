namespace ExpenseTracker.Domain.Abstractions;

public record ApplicationError(string Code, string Name)
{
    public static readonly ApplicationError None = new(string.Empty, string.Empty);
}
