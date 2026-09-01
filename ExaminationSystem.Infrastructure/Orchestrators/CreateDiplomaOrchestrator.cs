using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class CreateDiplomaOrchestrator : ICreateDiplomaOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateDiplomaOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<CreateDiplomaViewModel>> CreateAsync(
        CreateDiplomaCommand command,
        CancellationToken cancellationToken = default)
    {
        var instructor = await _userManager.FindByIdAsync(command.InstructorId.ToString());
        if (instructor is not Instructor)
        {
            return Result<CreateDiplomaViewModel>.Failure("Instructor was not found.");
        }

        var diplomaTitleExists = await _unitOfWork.Repository<Diploma>()
            .AnyAsync(x => x.Title == command.Title.Trim(), cancellationToken);

        if (diplomaTitleExists)
        {
            return Result<CreateDiplomaViewModel>.Failure("Diploma title already exists.");
        }

        var diploma = new Diploma
        {
            Title = command.Title.Trim(),
            Description = command.Description.Trim(),
            InstructorId = command.InstructorId,
            IsPublished = false
        };

        await _unitOfWork.Repository<Diploma>().AddAsync(diploma, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new DiplomaCreatedEvent(diploma.Id, diploma.Title, diploma.InstructorId),
            cancellationToken);

        var viewModel = new CreateDiplomaViewModel(
            diploma.Id,
            diploma.Title,
            diploma.Description,
            diploma.InstructorId,
            diploma.IsPublished);

        return Result<CreateDiplomaViewModel>.Success(viewModel);
    }
}
