using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest<Result<TaskDetailDto>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; }
    public TaskItemStatus Status { get; set; }
}
