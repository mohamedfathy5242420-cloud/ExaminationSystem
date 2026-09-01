using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetInstructorDashboardOrchestrator
{
    Task<Result<InstructorDashboardViewModel>> GetAsync(
        GetInstructorDashboardQuery query,
        CancellationToken cancellationToken = default);
}
