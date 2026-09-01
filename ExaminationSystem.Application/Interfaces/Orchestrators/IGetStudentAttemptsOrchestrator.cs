using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetStudentAttemptsOrchestrator
{
    Task<Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>> GetAsync(
        GetStudentAttemptsQuery query,
        CancellationToken cancellationToken = default);
}
