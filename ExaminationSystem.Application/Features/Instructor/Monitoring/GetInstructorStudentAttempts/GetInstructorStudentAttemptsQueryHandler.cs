using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;

public sealed class GetInstructorStudentAttemptsQueryHandler
    : IRequestHandler<GetInstructorStudentAttemptsQuery, Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>>
{
    private readonly IGetInstructorStudentAttemptsOrchestrator _orchestrator;

    public GetInstructorStudentAttemptsQueryHandler(IGetInstructorStudentAttemptsOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>> Handle(
        GetInstructorStudentAttemptsQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
