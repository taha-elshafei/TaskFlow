using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Shared.Auth.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IAuthService _authService;

    public LoginCommandHandler(
        IRepository<User> userRepository,
        IRepository<RefreshToken> refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _authService = authService;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result.Failure<AuthResponseDto>("Invalid email or password", 401);

        var roleNames = await _authService.GetUserRolesAsync(user.Id);
        if (roleNames.Count == 0) roleNames.Add("User");

        var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email, roleNames);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}
