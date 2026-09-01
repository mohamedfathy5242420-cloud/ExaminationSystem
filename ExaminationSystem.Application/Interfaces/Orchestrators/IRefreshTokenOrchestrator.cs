using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using ExaminationSystem.Application.Features.Auth.RefreshToken.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IRefreshTokenOrchestrator
{
    Task<Result<RefreshTokenViewModel>> RefreshAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default);
}
