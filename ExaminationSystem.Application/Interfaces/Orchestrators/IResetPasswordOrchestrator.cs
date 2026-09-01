using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
using ExaminationSystem.Application.Features.Auth.ResetPassword.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IResetPasswordOrchestrator
{
    Task<Result<ResetPasswordViewModel>> ResetAsync(
        ResetPasswordCommand command,
        CancellationToken cancellationToken = default);
}
