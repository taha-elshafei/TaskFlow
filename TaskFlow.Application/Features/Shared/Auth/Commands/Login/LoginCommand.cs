using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Login;

public class LoginCommand : IRequest<Result<AuthResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
