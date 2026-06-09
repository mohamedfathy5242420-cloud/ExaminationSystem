using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.Login.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface ILoginOrchestrator
{
    Task<Result<LoginViewModel>> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default);
}
