namespace ExpenseTracker.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess, ApplicationError error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public Result(bool isSuccess, IReadOnlyList<ApplicationError> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ApplicationError? Error { get; }
    public IReadOnlyList<ApplicationError>? Errors { get; }

    public static Result Ok() => new(true, ApplicationError.None);
    public static Result<TValue> Ok<TValue>(TValue value) => new(value, true, ApplicationError.None);
    public static Result Failure(ApplicationError error) => new(false, error);
    public static Result<TValue> Failure<TValue>(ApplicationError error) => new(false, error);
    public static Result Failure(IReadOnlyList<ApplicationError> errors) => new(false, errors);
    public static Result<TValue> Failure<TValue>(IReadOnlyList<ApplicationError> errors) => new(false, errors);
}

public sealed class Result<TValue> : Result
{
    public TValue? Value { get; }
    public Result(TValue value, bool isSuccess, ApplicationError error)
        : base(isSuccess, error)
    {
        Value = value;
    }
    public Result(bool isSuccess, IReadOnlyList<ApplicationError> errors)
        : base(isSuccess, errors)
    {
    }

    public Result(bool isSuccess, ApplicationError error)
        : base(isSuccess, error)
    {
    }
}