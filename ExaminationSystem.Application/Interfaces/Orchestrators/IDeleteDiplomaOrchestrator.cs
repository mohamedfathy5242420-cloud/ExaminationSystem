using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IDeleteDiplomaOrchestrator
{
    Task<Result<DeleteDiplomaViewModel>> DeleteAsync(
        DeleteDiplomaCommand command,
        CancellationToken cancellationToken = default);
}
