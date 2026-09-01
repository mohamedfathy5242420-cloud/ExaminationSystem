using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetInstructorDiplomasOrchestrator : IGetInstructorDiplomasOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInstructorDiplomasOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>> GetAsync(
        GetInstructorDiplomasQuery query,
        CancellationToken cancellationToken = default)
    {
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => x.InstructorId == query.InstructorId)
            .OrderBy(x => x.Title)
            .ToList();

        var diplomaIds = diplomas.Select(x => x.Id).ToList();
        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => diplomaIds.Contains(x.DiplomaId))
            .ToList();

        var enrollments = _unitOfWork.Repository<Enrollment>()
            .Query()
            .Where(x => diplomaIds.Contains(x.DiplomaId))
            .ToList();

        var viewModels = diplomas
            .Select(diploma => new InstructorDiplomaListItemViewModel(
                diploma.Id,
                diploma.Title,
                diploma.Description,
                diploma.IsPublished,
                quizzes.Count(x => x.DiplomaId == diploma.Id),
                quizzes.Count(x => x.DiplomaId == diploma.Id && x.IsPublished),
                enrollments.Count(x => x.DiplomaId == diploma.Id)))
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>.Success(viewModels));
    }
}
