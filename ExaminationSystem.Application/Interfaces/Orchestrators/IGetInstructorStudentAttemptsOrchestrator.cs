using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetInstructorStudentAttemptsOrchestrator
{
    Task<Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>> GetAsync(
        GetInstructorStudentAttemptsQuery query,
        CancellationToken cancellationToken = default);
}
