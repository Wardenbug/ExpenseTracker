namespace ExpenseTracker.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResponce> where TCommand : ICommand
{
    Task<TResponce> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
