using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Shared.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly IRepository<User> _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthService _authService;

    public GetCurrentUserQueryHandler(
        IRepository<User> userRepository,
        ICurrentUserService currentUserService,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _authService = authService;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
            return Result.Failure<UserDto>("Unauthorized", 401);

        var user = await _userRepository.GetByIdAsync(_currentUserService.UserId.Value);
        if (user is null)
            return Result.Failure<UserDto>("User not found", 404);

        var roles = await _authService.GetUserRolesAsync(user.Id);

        return Result.Success(new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Roles = roles
        });
    }
}
