using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Admin.Projects.Commands.CreateProject;
using TaskFlow.Application.Features.Admin.Projects.Commands.DeleteProject;
using TaskFlow.Application.Features.Admin.Projects.Commands.UpdateProject;
using TaskFlow.Application.Features.Admin.Projects.Queries.GetAllProjects;
using TaskFlow.Application.Features.Admin.Projects.Queries.GetProjectById;

namespace TaskFlow.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/projects")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProjectsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand { Id = id });
        return StatusCode(result.StatusCode, result);
    }
}
