using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Learning;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class DeleteDiplomaOrchestrator : IDeleteDiplomaOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public DeleteDiplomaOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<DeleteDiplomaViewModel>> DeleteAsync(
        DeleteDiplomaCommand command,
        CancellationToken cancellationToken = default)
    {
        var diploma = await _unitOfWork.Repository<Diploma>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (diploma is null)
        {
            return Result<DeleteDiplomaViewModel>.Failure("Diploma was not found.");
        }

        _unitOfWork.Repository<Diploma>().Delete(diploma);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new DiplomaDeletedEvent(diploma.Id, diploma.Title),
            cancellationToken);

        var viewModel = new DeleteDiplomaViewModel(
            diploma.Id,
            "Diploma deleted successfully.");

        return Result<DeleteDiplomaViewModel>.Success(viewModel);
    }
}
