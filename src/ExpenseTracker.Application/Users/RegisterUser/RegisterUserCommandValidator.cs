using FluentValidation;

namespace ExpenseTracker.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command =>
            command.Email).EmailAddress();

        RuleFor(command =>
            command.UserName)
            .NotEmpty();

        RuleFor(command =>
            command.Password)
            .NotEmpty();

        RuleFor(command =>
            command.ConfirmPassword)
            .Equal(command => command.Password)
                .WithMessage("Passwords do not match.");
    }
}
