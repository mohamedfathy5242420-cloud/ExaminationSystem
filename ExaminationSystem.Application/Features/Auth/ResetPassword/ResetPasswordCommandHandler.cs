using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ResetPassword.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandHandler
    : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordViewModel>>
{
    private readonly IResetPasswordOrchestrator _resetPasswordOrchestrator;

    public ResetPasswordCommandHandler(IResetPasswordOrchestrator resetPasswordOrchestrator)
    {
        _resetPasswordOrchestrator = resetPasswordOrchestrator;
    }

    public Task<Result<ResetPasswordViewModel>> Handle(
        ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return _resetPasswordOrchestrator.ResetAsync(command, cancellationToken);
    }
}
