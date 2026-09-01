using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetQuizHistoryOrchestrator : IGetQuizHistoryOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetQuizHistoryOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<QuizHistoryItemViewModel>>> GetAsync(
        GetQuizHistoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var attemptsQuery = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => x.StudentId == query.StudentId);

        if (query.QuizId.HasValue)
        {
            attemptsQuery = attemptsQuery.Where(x => x.QuizId == query.QuizId.Value);
        }

        var attempts = attemptsQuery
            .OrderByDescending(x => x.StartTime)
            .ToList();

        var quizIds = attempts.Select(x => x.QuizId).Distinct().ToList();
        var quizzesQuery = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => quizIds.Contains(x.Id));

        if (query.DiplomaId.HasValue)
        {
            quizzesQuery = quizzesQuery.Where(x => x.DiplomaId == query.DiplomaId.Value);
        }

        var quizzes = quizzesQuery.ToList();
        var quizIdsAfterDiplomaFilter = quizzes.Select(x => x.Id).ToHashSet();

        var viewModels = attempts
            .Where(x => quizIdsAfterDiplomaFilter.Contains(x.QuizId))
            .Select(attempt =>
            {
                var quiz = quizzes.First(x => x.Id == attempt.QuizId);

                return new QuizHistoryItemViewModel(
                    attempt.Id,
                    quiz.Id,
                    quiz.Title,
                    quiz.DiplomaId,
                    attempt.Score,
                    quiz.PassScore,
                    attempt.IsPassed,
                    attempt.Status.ToString(),
                    attempt.StartTime,
                    attempt.EndTime);
            })
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<QuizHistoryItemViewModel>>.Success(viewModels));
    }
}
