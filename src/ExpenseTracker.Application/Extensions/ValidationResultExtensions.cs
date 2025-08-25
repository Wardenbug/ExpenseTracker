using ExpenseTracker.Domain.Abstractions;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.Extensions;

internal static class ValidationResultExtensions
{
    public static List<ApplicationError> ToApplicationErrors(this List<ValidationFailure> failures)
    {
        var applicationErrors = failures.Select(error =>
            new ApplicationError(
                error.PropertyName,
                error.ErrorMessage));

        return applicationErrors.ToList();

    }   
}
