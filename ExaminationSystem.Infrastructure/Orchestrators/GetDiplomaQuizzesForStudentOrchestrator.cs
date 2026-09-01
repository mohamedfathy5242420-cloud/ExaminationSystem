using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetDiplomaQuizzesForStudentOrchestrator : IGetDiplomaQuizzesForStudentOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDiplomaQuizzesForStudentOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<StudentDiplomaQuizViewModel>>> GetAsync(
        GetDiplomaQuizzesForStudentQuery query,
        CancellationToken cancellationToken = default)
    {
        var diploma = _unitOfWork.Repository<Diploma>()
            .Query()
            .FirstOrDefault(x => x.Id == query.DiplomaId && x.IsPublished);

        if (diploma is null)
        {
            return Task.FromResult(
                Result<IReadOnlyList<StudentDiplomaQuizViewModel>>.Failure("Diploma was not found."));
        }

        var isEnrolled = _unitOfWork.Repository<Enrollment>()
            .Query()
            .Any(x => x.StudentId == query.StudentId && x.DiplomaId == query.DiplomaId);

        if (!isEnrolled)
        {
            return Task.FromResult(
                Result<IReadOnlyList<StudentDiplomaQuizViewModel>>.Failure("Student is not enrolled in this diploma."));
        }

        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => x.DiplomaId == query.DiplomaId && x.IsPublished)
            .OrderBy(x => x.Title)
            .ToList();

        var quizIds = quizzes.Select(x => x.Id).ToList();
        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => x.StudentId == query.StudentId && quizIds.Contains(x.QuizId))
            .OrderByDescending(x => x.StartTime)
            .ToList();

        var viewModels = quizzes
            .Select(quiz =>
            {
                var quizAttempts = attempts
                    .Where(x => x.QuizId == quiz.Id)
                    .ToList();

                return new StudentDiplomaQuizViewModel(
                    quiz.Id,
                    quiz.Title,
                    quiz.Duration,
                    quiz.PassScore,
                    quiz.MaxAttempts,
                    quiz.Instructions,
                    quizAttempts.Count,
                    Math.Max(0, quiz.MaxAttempts - quizAttempts.Count),
                    quizAttempts
                        .Select(attempt => new StudentQuizAttemptSummaryViewModel(
                            attempt.Id,
                            attempt.Score,
                            attempt.IsPassed,
                            attempt.Status.ToString(),
                            attempt.StartTime,
                            attempt.EndTime))
                        .ToList());
            })
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<StudentDiplomaQuizViewModel>>.Success(viewModels));
    }
}
