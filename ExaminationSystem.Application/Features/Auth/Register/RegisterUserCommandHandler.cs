using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Register.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.Register;

public sealed class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Result<RegisterUserViewModel>>
{
    private readonly IRegisterUserOrchestrator _registerUserOrchestrator;

    public RegisterUserCommandHandler(IRegisterUserOrchestrator registerUserOrchestrator)
    {
        _registerUserOrchestrator = registerUserOrchestrator;
    }

    public Task<Result<RegisterUserViewModel>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        return _registerUserOrchestrator.RegisterAsync(command, cancellationToken);
    }
}
