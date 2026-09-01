using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;

public sealed class GetStudentAttemptsQueryHandler
    : IRequestHandler<GetStudentAttemptsQuery, Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>>
{
    private readonly IGetStudentAttemptsOrchestrator _orchestrator;

    public GetStudentAttemptsQueryHandler(IGetStudentAttemptsOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>> Handle(
        GetStudentAttemptsQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
