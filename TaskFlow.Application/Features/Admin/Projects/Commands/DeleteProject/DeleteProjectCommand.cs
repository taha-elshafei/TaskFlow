using MediatR;
using TaskFlow.Application.Common;

namespace TaskFlow.Application.Features.Admin.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}
