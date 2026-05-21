using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Admin.Tasks.Commands.CreateTask;
using TaskFlow.Application.Features.Admin.Tasks.Commands.DeleteTask;
using TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTask;
using TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTaskStatus;
using TaskFlow.Application.Features.Admin.Tasks.Queries.GetAllTasks;
using TaskFlow.Application.Features.Admin.Tasks.Queries.GetTaskById;
using TaskFlow.Application.Features.Admin.Tasks.Queries.GetTasksByProject;

namespace TaskFlow.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TasksController> _logger;

    public TasksController(IMediator mediator, ILogger<TasksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTasksQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        _logger.LogInformation("Creating task for project {ProjectId}", command.ProjectId);
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> GetByProject(Guid projectId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        var query = new GetTasksByProjectQuery
        {
            ProjectId = projectId,
            Page = page,
            PageSize = pageSize,
            Search = search
        };
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery { Id = id });
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand { Id = id });
        return StatusCode(result.StatusCode, result);
    }
}
