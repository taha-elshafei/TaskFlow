using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;

namespace TaskFlow.Application.Features.Shared.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<Result<UserDto>>
{
}
