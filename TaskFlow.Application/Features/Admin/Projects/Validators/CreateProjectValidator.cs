using FluentValidation;
using TaskFlow.Application.Features.Admin.Projects.Commands.CreateProject;

namespace TaskFlow.Application.Features.Admin.Projects.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}
