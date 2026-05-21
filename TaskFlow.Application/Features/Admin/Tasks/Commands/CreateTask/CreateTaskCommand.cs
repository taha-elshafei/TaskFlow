using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Admin.Tasks.Commands.CreateTask;

public class CreateTaskCommand : IRequest<Result<TaskDetailDto>>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public Guid ProjectId { get; set; }
}
