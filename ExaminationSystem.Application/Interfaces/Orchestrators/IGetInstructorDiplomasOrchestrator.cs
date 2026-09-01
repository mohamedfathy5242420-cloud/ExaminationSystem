using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetInstructorDiplomasOrchestrator
{
    Task<Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>> GetAsync(
        GetInstructorDiplomasQuery query,
        CancellationToken cancellationToken = default);
}
