using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Application.Features.Auth.ForgotPassword.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IForgotPasswordOrchestrator
{
    Task<Result<ForgotPasswordViewModel>> SendResetCodeAsync(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken = default);
}
