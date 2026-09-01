using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface ICreateDiplomaOrchestrator
{
    Task<Result<CreateDiplomaViewModel>> CreateAsync(
        CreateDiplomaCommand command,
        CancellationToken cancellationToken = default);
}
