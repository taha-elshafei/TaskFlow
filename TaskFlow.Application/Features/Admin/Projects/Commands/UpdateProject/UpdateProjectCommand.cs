using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;

namespace TaskFlow.Application.Features.Admin.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<Result<ProjectDetailDto>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
