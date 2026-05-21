using FluentValidation;
using TaskFlow.Application.Features.Shared.Auth.Commands.Register;

namespace TaskFlow.Application.Features.Shared.Auth.Validators;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Not a valid email");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches(@"(?=.*[A-Z])(?=.*[a-z])(?=.*\d)").WithMessage("Password must contain uppercase, lowercase, and a digit");
    }
}
