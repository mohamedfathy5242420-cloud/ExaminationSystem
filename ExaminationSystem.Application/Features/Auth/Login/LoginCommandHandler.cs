using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Login.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginViewModel>>
{
    private readonly ILoginOrchestrator _loginOrchestrator;

    public LoginCommandHandler(ILoginOrchestrator loginOrchestrator)
    {
        _loginOrchestrator = loginOrchestrator;
    }

    public Task<Result<LoginViewModel>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        return _loginOrchestrator.LoginAsync(command, cancellationToken);
    }
}
