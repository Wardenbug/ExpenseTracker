namespace ExpenseTracker.Application.Abstractions;

public interface IQueryHandler<in TCommand, TResponce> where TCommand : ICommand
{
    Task<TResponce> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

