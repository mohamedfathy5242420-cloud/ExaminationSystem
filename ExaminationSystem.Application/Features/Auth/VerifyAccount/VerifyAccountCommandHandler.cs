using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.VerifyAccount.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.VerifyAccount;

public sealed class VerifyAccountCommandHandler
    : IRequestHandler<VerifyAccountCommand, Result<VerifyAccountViewModel>>
{
    private readonly IVerifyAccountOrchestrator _verifyAccountOrchestrator;

    public VerifyAccountCommandHandler(IVerifyAccountOrchestrator verifyAccountOrchestrator)
    {
        _verifyAccountOrchestrator = verifyAccountOrchestrator;
    }

    public Task<Result<VerifyAccountViewModel>> Handle(
        VerifyAccountCommand command,
        CancellationToken cancellationToken)
    {
        return _verifyAccountOrchestrator.VerifyAsync(command, cancellationToken);
    }
}
