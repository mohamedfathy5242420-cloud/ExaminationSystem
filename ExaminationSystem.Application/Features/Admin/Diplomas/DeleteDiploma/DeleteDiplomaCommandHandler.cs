using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;

public sealed class DeleteDiplomaCommandHandler
    : IRequestHandler<DeleteDiplomaCommand, Result<DeleteDiplomaViewModel>>
{
    private readonly IDeleteDiplomaOrchestrator _deleteDiplomaOrchestrator;

    public DeleteDiplomaCommandHandler(IDeleteDiplomaOrchestrator deleteDiplomaOrchestrator)
    {
        _deleteDiplomaOrchestrator = deleteDiplomaOrchestrator;
    }

    public Task<Result<DeleteDiplomaViewModel>> Handle(
        DeleteDiplomaCommand command,
        CancellationToken cancellationToken)
    {
        return _deleteDiplomaOrchestrator.DeleteAsync(command, cancellationToken);
    }
}
