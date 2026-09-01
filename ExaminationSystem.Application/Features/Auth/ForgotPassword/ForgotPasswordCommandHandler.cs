using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ForgotPassword.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordViewModel>>
{
    private readonly IForgotPasswordOrchestrator _forgotPasswordOrchestrator;

    public ForgotPasswordCommandHandler(IForgotPasswordOrchestrator forgotPasswordOrchestrator)
    {
        _forgotPasswordOrchestrator = forgotPasswordOrchestrator;
    }

    public Task<Result<ForgotPasswordViewModel>> Handle(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return _forgotPasswordOrchestrator.SendResetCodeAsync(command, cancellationToken);
    }
}
