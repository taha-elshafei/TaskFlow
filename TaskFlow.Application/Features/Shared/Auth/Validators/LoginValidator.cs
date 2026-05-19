using FluentValidation;
using TaskFlow.Application.Features.Shared.Auth.Commands.Login;

namespace TaskFlow.Application.Features.Shared.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
