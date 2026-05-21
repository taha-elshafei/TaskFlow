using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Shared.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(IRepository<RefreshToken> refreshTokenRepository, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var tokens = await _refreshTokenRepository.FindAsync(rt => rt.Token == request.RefreshToken && !rt.IsRevoked);
        var token = tokens.FirstOrDefault();

        if (token == null)
            return Result.Failure("Token not found or already revoked", 400);

        token.IsRevoked = true;
        _refreshTokenRepository.Update(token);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
