using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;

namespace TaskFlow.Application.Features.Admin.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Result<ProjectDetailDto>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
