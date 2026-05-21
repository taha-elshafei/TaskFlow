using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;

namespace TaskFlow.Application.Features.Admin.Tasks.Queries.GetAllTasks;

public class GetAllTasksQuery : PagedQuery, IRequest<Result<PagedResult<TaskListDto>>>
{
}
