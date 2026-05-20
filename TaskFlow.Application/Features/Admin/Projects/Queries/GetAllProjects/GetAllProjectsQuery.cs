using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;

namespace TaskFlow.Application.Features.Admin.Projects.Queries.GetAllProjects;

public class GetAllProjectsQuery : PagedQuery, IRequest<Result<PagedResult<ProjectListDto>>>
{
}
