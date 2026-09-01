using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class BrowseDiplomasOrchestrator : IBrowseDiplomasOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public BrowseDiplomasOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<StudentDiplomaListItemViewModel>>> GetAsync(
        BrowseDiplomasQuery query,
        CancellationToken cancellationToken = default)
    {
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => x.IsPublished)
            .OrderBy(x => x.Title)
            .ToList();

        var diplomaIds = diplomas.Select(x => x.Id).ToList();
        var enrollments = _unitOfWork.Repository<Enrollment>()
            .Query()
            .Where(x => x.StudentId == query.StudentId && diplomaIds.Contains(x.DiplomaId))
            .ToList();

        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => diplomaIds.Contains(x.DiplomaId) && x.IsPublished)
            .ToList();

        var quizIds = quizzes.Select(x => x.Id).ToList();
        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => x.StudentId == query.StudentId && quizIds.Contains(x.QuizId))
            .ToList();

        var viewModels = diplomas
            .Select(diploma =>
            {
                var enrollment = enrollments.FirstOrDefault(x => x.DiplomaId == diploma.Id);
                var diplomaQuizIds = quizzes
                    .Where(x => x.DiplomaId == diploma.Id)
                    .Select(x => x.Id)
                    .ToList();

                return new StudentDiplomaListItemViewModel(
                    diploma.Id,
                    diploma.Title,
                    diploma.Description,
                    diploma.InstructorId,
                    enrollment is not null,
                    enrollment?.Progress ?? 0,
                    diplomaQuizIds.Count,
                    attempts.Count(x => diplomaQuizIds.Contains(x.QuizId)
                        && x.Status != AttemptStatus.InProgress));
            })
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<StudentDiplomaListItemViewModel>>.Success(viewModels));
    }
}
