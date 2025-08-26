namespace ExpenseTracker.Application.Abstractions;

public interface IQueryHandler<in TQuery, TResponce> where TQuery : IQuery
{
    Task<TResponce> HandleAsync(TQuery command, CancellationToken cancellationToken = default);
}

