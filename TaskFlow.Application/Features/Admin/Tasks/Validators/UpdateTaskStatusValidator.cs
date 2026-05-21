using FluentValidation;
using TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTaskStatus;

namespace TaskFlow.Application.Features.Admin.Tasks.Validators;

public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
