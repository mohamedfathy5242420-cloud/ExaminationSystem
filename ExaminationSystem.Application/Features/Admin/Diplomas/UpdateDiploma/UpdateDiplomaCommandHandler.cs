using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;

public sealed class UpdateDiplomaCommandHandler
    : IRequestHandler<UpdateDiplomaCommand, Result<UpdateDiplomaViewModel>>
{
    private readonly IUpdateDiplomaOrchestrator _updateDiplomaOrchestrator;

    public UpdateDiplomaCommandHandler(IUpdateDiplomaOrchestrator updateDiplomaOrchestrator)
    {
        _updateDiplomaOrchestrator = updateDiplomaOrchestrator;
    }

    public Task<Result<UpdateDiplomaViewModel>> Handle(
        UpdateDiplomaCommand command,
        CancellationToken cancellationToken)
    {
        return _updateDiplomaOrchestrator.UpdateAsync(command, cancellationToken);
    }
}
