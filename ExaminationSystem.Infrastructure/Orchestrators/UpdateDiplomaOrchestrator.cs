using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class UpdateDiplomaOrchestrator : IUpdateDiplomaOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public UpdateDiplomaOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<UpdateDiplomaViewModel>> UpdateAsync(
        UpdateDiplomaCommand command,
        CancellationToken cancellationToken = default)
    {
        var diploma = await _unitOfWork.Repository<Diploma>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (diploma is null)
        {
            return Result<UpdateDiplomaViewModel>.Failure("Diploma was not found.");
        }

        var instructor = await _userManager.FindByIdAsync(command.InstructorId.ToString());
        if (instructor is not Instructor)
        {
            return Result<UpdateDiplomaViewModel>.Failure("Instructor was not found.");
        }

        var title = command.Title.Trim();
        var titleExists = await _unitOfWork.Repository<Diploma>()
            .AnyAsync(x => x.Id != diploma.Id && x.Title == title, cancellationToken);

        if (titleExists)
        {
            return Result<UpdateDiplomaViewModel>.Failure("Diploma title already exists.");
        }

        diploma.Title = title;
        diploma.Description = command.Description.Trim();
        diploma.InstructorId = command.InstructorId;

        _unitOfWork.Repository<Diploma>().Update(diploma);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new DiplomaUpdatedEvent(diploma.Id, diploma.Title, diploma.InstructorId),
            cancellationToken);

        var viewModel = new UpdateDiplomaViewModel(
            diploma.Id,
            diploma.Title,
            diploma.Description,
            diploma.InstructorId,
            diploma.IsPublished);

        return Result<UpdateDiplomaViewModel>.Success(viewModel);
    }
}
