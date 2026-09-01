using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IEnrollInDiplomaOrchestrator
{
    Task<Result<EnrollInDiplomaViewModel>> EnrollAsync(
        EnrollInDiplomaCommand command,
        CancellationToken cancellationToken = default);
}
