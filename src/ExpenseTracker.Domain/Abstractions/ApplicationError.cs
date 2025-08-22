namespace ExpenseTracker.Domain.Abstractions;

public record ApplicationError(string Code, string Message)
{
    public static readonly ApplicationError None = new(string.Empty, string.Empty);
}

public static class ApplicationErrorExtensions  
{
    public static Dictionary<string, string[]> 
        ToValidationErrors(this IReadOnlyList<ApplicationError> applicationErrors)
    {
        return applicationErrors.GroupBy(e => e.Code, e => e.Message)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToArray()
                )!;
    }
}