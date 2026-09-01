using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetStudentAttemptDetailsOrchestrator
{
    Task<Result<AdminStudentAttemptDetailsViewModel>> GetAsync(
        GetStudentAttemptDetailsQuery query,
        CancellationToken cancellationToken = default);
}
