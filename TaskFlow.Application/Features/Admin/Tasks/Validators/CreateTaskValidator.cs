using FluentValidation;
using TaskFlow.Application.Features.Admin.Tasks.Commands.CreateTask;

namespace TaskFlow.Application.Features.Admin.Tasks.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority is not valid");
    }
}
