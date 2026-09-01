using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IBrowseDiplomasOrchestrator
{
    Task<Result<IReadOnlyList<StudentDiplomaListItemViewModel>>> GetAsync(
        BrowseDiplomasQuery query,
        CancellationToken cancellationToken = default);
}
