using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IUpdateDiplomaOrchestrator
{
    Task<Result<UpdateDiplomaViewModel>> UpdateAsync(
        UpdateDiplomaCommand command,
        CancellationToken cancellationToken = default);
}
