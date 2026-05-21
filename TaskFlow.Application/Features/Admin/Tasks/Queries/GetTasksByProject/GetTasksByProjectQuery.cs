using System.Text.Json.Serialization;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;

namespace TaskFlow.Application.Features.Admin.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectQuery : PagedQuery, IRequest<Result<PagedResult<TaskListDto>>>
{
    [JsonIgnore]
    public Guid ProjectId { get; set; }
}
