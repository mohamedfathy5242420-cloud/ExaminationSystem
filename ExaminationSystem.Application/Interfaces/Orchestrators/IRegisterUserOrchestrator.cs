using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.Register.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IRegisterUserOrchestrator
{
    Task<Result<RegisterUserViewModel>> RegisterAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default);
}
