using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;

public sealed class GetStudentDashboardQueryHandler
    : IRequestHandler<GetStudentDashboardQuery, Result<StudentDashboardViewModel>>
{
    private readonly IGetStudentDashboardOrchestrator _getStudentDashboardOrchestrator;

    public GetStudentDashboardQueryHandler(IGetStudentDashboardOrchestrator getStudentDashboardOrchestrator)
    {
        _getStudentDashboardOrchestrator = getStudentDashboardOrchestrator;
    }

    public Task<Result<StudentDashboardViewModel>> Handle(
        GetStudentDashboardQuery query,
        CancellationToken cancellationToken)
    {
        return _getStudentDashboardOrchestrator.GetAsync(query, cancellationToken);
    }
}
