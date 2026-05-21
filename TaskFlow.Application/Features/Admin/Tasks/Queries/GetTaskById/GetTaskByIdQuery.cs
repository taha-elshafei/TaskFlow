using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;

namespace TaskFlow.Application.Features.Admin.Tasks.Queries.GetTaskById;

public class GetTaskByIdQuery : IRequest<Result<TaskDetailDto>>
{
    public Guid Id { get; set; }
}
