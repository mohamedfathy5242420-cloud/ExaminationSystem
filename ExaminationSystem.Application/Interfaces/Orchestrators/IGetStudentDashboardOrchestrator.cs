using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetStudentDashboardOrchestrator
{
    Task<Result<StudentDashboardViewModel>> GetAsync(
        GetStudentDashboardQuery query,
        CancellationToken cancellationToken = default);
}
