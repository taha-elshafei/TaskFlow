using FluentValidation;
using TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTask;

namespace TaskFlow.Application.Features.Admin.Tasks.Validators;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Priority)
            .IsInEnum();

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status value is not valid");
    }
}
