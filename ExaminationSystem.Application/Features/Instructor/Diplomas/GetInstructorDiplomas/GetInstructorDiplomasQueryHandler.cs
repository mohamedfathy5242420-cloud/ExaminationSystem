using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;

public sealed class GetInstructorDiplomasQueryHandler
    : IRequestHandler<GetInstructorDiplomasQuery, Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>>
{
    private readonly IGetInstructorDiplomasOrchestrator _orchestrator;

    public GetInstructorDiplomasQueryHandler(IGetInstructorDiplomasOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>> Handle(
        GetInstructorDiplomasQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
