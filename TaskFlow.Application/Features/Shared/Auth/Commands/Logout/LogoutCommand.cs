using MediatR;
using TaskFlow.Application.Common;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Logout;

public class LogoutCommand : IRequest<Result>
{
    public string RefreshToken { get; set; } = string.Empty;
}
