using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Refresh;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result<AuthResponseDto>>
{
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IAuthService _authService;

    public RefreshCommandHandler(
        IRepository<RefreshToken> refreshTokenRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IAuthService authService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _authService = authService;
    }

    public async Task<Result<AuthResponseDto>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var tokens = await _refreshTokenRepository.FindAsync(rt =>
            rt.Token == request.RefreshToken && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow);
        var existingToken = tokens.FirstOrDefault();

        if (existingToken is null)
            return Result.Failure<AuthResponseDto>("Invalid or expired refresh token", 401);

        existingToken.IsRevoked = true;
        _refreshTokenRepository.Update(existingToken);

        var user = await _userRepository.GetByIdAsync(existingToken.UserId);
        if (user is null)
            return Result.Failure<AuthResponseDto>("User not found", 404);

        var roleNames = await _authService.GetUserRolesAsync(user.Id);
        if (roleNames.Count == 0) roleNames.Add("User");

        var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email, roleNames);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        });
    }
}
