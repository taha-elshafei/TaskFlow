using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommand : IRequest<Result<TaskDetailDto>>
{
    public Guid Id { get; set; }
    public TaskItemStatus Status { get; set; }
}
