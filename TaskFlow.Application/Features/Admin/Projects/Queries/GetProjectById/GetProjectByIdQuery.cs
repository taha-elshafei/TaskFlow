using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;

namespace TaskFlow.Application.Features.Admin.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<Result<ProjectDetailDto>>
{
    public Guid Id { get; set; }
}
