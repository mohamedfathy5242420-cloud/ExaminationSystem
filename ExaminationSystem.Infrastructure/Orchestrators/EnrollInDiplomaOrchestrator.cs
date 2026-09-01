using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class EnrollInDiplomaOrchestrator : IEnrollInDiplomaOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly UserManager<User> _userManager;

    public EnrollInDiplomaOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
        _userManager = userManager;
    }

    public async Task<Result<EnrollInDiplomaViewModel>> EnrollAsync(
        EnrollInDiplomaCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.StudentId.ToString());
        if (user is not Student)
        {
            return Result<EnrollInDiplomaViewModel>.Failure("Student was not found.");
        }

        var diploma = await _unitOfWork.Repository<Diploma>()
            .FirstOrDefaultAsync(x => x.Id == command.DiplomaId, cancellationToken);

        if (diploma is null)
        {
            return Result<EnrollInDiplomaViewModel>.Failure("Diploma was not found.");
        }

        if (!diploma.IsPublished)
        {
            return Result<EnrollInDiplomaViewModel>.Failure("Diploma is not published.");
        }

        var alreadyEnrolled = await _unitOfWork.Repository<Enrollment>()
            .AnyAsync(
                x => x.StudentId == command.StudentId && x.DiplomaId == command.DiplomaId,
                cancellationToken);

        if (alreadyEnrolled)
        {
            return Result<EnrollInDiplomaViewModel>.Failure("Student is already enrolled in this diploma.");
        }

        var enrolledAt = DateTime.UtcNow;
        var enrollment = new Enrollment
        {
            StudentId = command.StudentId,
            DiplomaId = command.DiplomaId,
            EnrolledAt = enrolledAt,
            Progress = 0
        };

        await _unitOfWork.Repository<Enrollment>().AddAsync(enrollment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new StudentEnrolledInDiplomaEvent(
                enrollment.Id,
                enrollment.StudentId,
                enrollment.DiplomaId,
                enrollment.EnrolledAt),
            cancellationToken);

        var viewModel = new EnrollInDiplomaViewModel(
            enrollment.Id,
            enrollment.StudentId,
            enrollment.DiplomaId,
            enrollment.EnrolledAt,
            enrollment.Progress);

        return Result<EnrollInDiplomaViewModel>.Success(viewModel);
    }
}
