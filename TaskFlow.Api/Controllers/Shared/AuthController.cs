using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Shared.Auth.Commands.Login;
using TaskFlow.Application.Features.Shared.Auth.Commands.Logout;
using TaskFlow.Application.Features.Shared.Auth.Commands.Refresh;
using TaskFlow.Application.Features.Shared.Auth.Commands.Register;
using TaskFlow.Application.Features.Shared.Auth.Queries.GetCurrentUser;

namespace TaskFlow.Api.Controllers.Shared;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _mediator.Send(new GetCurrentUserQuery());
        return StatusCode(result.StatusCode, result);
    }
}
