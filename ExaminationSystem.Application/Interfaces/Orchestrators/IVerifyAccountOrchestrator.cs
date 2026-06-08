using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using ExaminationSystem.Application.Features.Auth.VerifyAccount.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IVerifyAccountOrchestrator
{
    Task<Result<VerifyAccountViewModel>> VerifyAsync(
        VerifyAccountCommand command,
        CancellationToken cancellationToken = default);
}
