using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;

public sealed class CreateDiplomaCommandHandler
    : IRequestHandler<CreateDiplomaCommand, Result<CreateDiplomaViewModel>>
{
    private readonly ICreateDiplomaOrchestrator _createDiplomaOrchestrator;

    public CreateDiplomaCommandHandler(ICreateDiplomaOrchestrator createDiplomaOrchestrator)
    {
        _createDiplomaOrchestrator = createDiplomaOrchestrator;
    }

    public Task<Result<CreateDiplomaViewModel>> Handle(
        CreateDiplomaCommand command,
        CancellationToken cancellationToken)
    {
        return _createDiplomaOrchestrator.CreateAsync(command, cancellationToken);
    }
}
