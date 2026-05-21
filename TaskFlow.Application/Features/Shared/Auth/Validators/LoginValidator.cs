using FluentValidation;
using TaskFlow.Application.Features.Shared.Auth.Commands.Login;

namespace TaskFlow.Application.Features.Shared.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
