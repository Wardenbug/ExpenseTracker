namespace ExpenseTracker.Domain.Abstractions;

public sealed record PaginationResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = new List<T>();
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int TotalCount { get; init; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}
