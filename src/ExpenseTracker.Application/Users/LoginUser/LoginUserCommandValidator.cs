using FluentValidation;

namespace ExpenseTracker.Application.Users.LoginUser;

internal sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command =>
           command.UserName)
           .NotEmpty();

        RuleFor(command =>
            command.Password)
            .NotEmpty();
    }
}
