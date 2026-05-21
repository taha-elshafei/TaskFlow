using FluentValidation;
using TaskFlow.Application.Features.Admin.Projects.Commands.UpdateProject;

namespace TaskFlow.Application.Features.Admin.Projects.Validators;

public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}
