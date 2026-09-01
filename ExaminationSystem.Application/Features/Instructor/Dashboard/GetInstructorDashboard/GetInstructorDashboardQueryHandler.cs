using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;

public sealed class GetInstructorDashboardQueryHandler
    : IRequestHandler<GetInstructorDashboardQuery, Result<InstructorDashboardViewModel>>
{
    private readonly IGetInstructorDashboardOrchestrator _orchestrator;

    public GetInstructorDashboardQueryHandler(IGetInstructorDashboardOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<InstructorDashboardViewModel>> Handle(
        GetInstructorDashboardQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
