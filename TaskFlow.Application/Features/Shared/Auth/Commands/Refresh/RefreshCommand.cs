using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Refresh;

public class RefreshCommand : IRequest<Result<AuthResponseDto>>
{
    public string RefreshToken { get; set; } = string.Empty;
}
