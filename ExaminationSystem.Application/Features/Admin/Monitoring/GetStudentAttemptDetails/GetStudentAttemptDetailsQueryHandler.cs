using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;

public sealed class GetStudentAttemptDetailsQueryHandler
    : IRequestHandler<GetStudentAttemptDetailsQuery, Result<AdminStudentAttemptDetailsViewModel>>
{
    private readonly IGetStudentAttemptDetailsOrchestrator _orchestrator;

    public GetStudentAttemptDetailsQueryHandler(IGetStudentAttemptDetailsOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<AdminStudentAttemptDetailsViewModel>> Handle(
        GetStudentAttemptDetailsQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
