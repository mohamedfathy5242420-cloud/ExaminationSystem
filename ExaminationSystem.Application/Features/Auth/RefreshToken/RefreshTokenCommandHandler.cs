using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.RefreshToken.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenViewModel>>
{
    private readonly IRefreshTokenOrchestrator _refreshTokenOrchestrator;

    public RefreshTokenCommandHandler(IRefreshTokenOrchestrator refreshTokenOrchestrator)
    {
        _refreshTokenOrchestrator = refreshTokenOrchestrator;
    }

    public Task<Result<RefreshTokenViewModel>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        return _refreshTokenOrchestrator.RefreshAsync(command, cancellationToken);
    }
}
