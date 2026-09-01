using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;

public sealed class EnrollInDiplomaCommandHandler
    : IRequestHandler<EnrollInDiplomaCommand, Result<EnrollInDiplomaViewModel>>
{
    private readonly IEnrollInDiplomaOrchestrator _enrollInDiplomaOrchestrator;

    public EnrollInDiplomaCommandHandler(IEnrollInDiplomaOrchestrator enrollInDiplomaOrchestrator)
    {
        _enrollInDiplomaOrchestrator = enrollInDiplomaOrchestrator;
    }

    public Task<Result<EnrollInDiplomaViewModel>> Handle(
        EnrollInDiplomaCommand command,
        CancellationToken cancellationToken)
    {
        return _enrollInDiplomaOrchestrator.EnrollAsync(command, cancellationToken);
    }
}
