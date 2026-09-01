using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Learning;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetPerformanceAnalyticsOrchestrator : IGetPerformanceAnalyticsOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPerformanceAnalyticsOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<PerformanceAnalyticsViewModel>> GetAsync(
        GetPerformanceAnalyticsQuery query,
        CancellationToken cancellationToken = default)
    {
        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .ToList();

        var completedAttempts = attempts
            .Where(x => x.Status != AttemptStatus.InProgress)
            .ToList();

        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .ToList();

        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .ToList();

        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .ToList();

        var answers = _unitOfWork.Repository<AttemptAnswer>()
            .Query()
            .Where(x => x.SelectedOptionId.HasValue)
            .ToList();

        var optionIds = answers.Select(x => x.SelectedOptionId!.Value).Distinct().ToList();
        var selectedOptions = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => optionIds.Contains(x.Id))
            .ToList();

        var passRateByQuiz = quizzes
            .Select(quiz =>
            {
                var quizAttempts = completedAttempts
                    .Where(x => x.QuizId == quiz.Id)
                    .ToList();
                var passedCount = quizAttempts.Count(x => x.IsPassed);

                return new QuizPassRateViewModel(
                    quiz.Id,
                    quiz.Title,
                    quizAttempts.Count,
                    passedCount,
                    quizAttempts.Count == 0
                        ? 0
                        : Math.Round((decimal)passedCount / quizAttempts.Count * 100, 2));
            })
            .ToList();

        var averageScoreByDiploma = diplomas
            .Select(diploma =>
            {
                var diplomaQuizIds = quizzes
                    .Where(x => x.DiplomaId == diploma.Id)
                    .Select(x => x.Id)
                    .ToList();
                var diplomaAttempts = completedAttempts
                    .Where(x => diplomaQuizIds.Contains(x.QuizId))
                    .ToList();

                return new DiplomaAverageScoreViewModel(
                    diploma.Id,
                    diploma.Title,
                    diplomaAttempts.Count == 0
                        ? 0
                        : Math.Round((decimal)diplomaAttempts.Average(x => x.Score), 2));
            })
            .ToList();

        var attemptsOverTime = attempts
            .GroupBy(x => DateOnly.FromDateTime(x.StartTime.Date))
            .OrderBy(x => x.Key)
            .Select(group => new AttemptsOverTimeViewModel(
                group.Key,
                group.Count()))
            .ToList();

        var failedQuestionCounts = answers
            .Select(answer =>
            {
                var selectedOption = selectedOptions.FirstOrDefault(x => x.Id == answer.SelectedOptionId);

                return new
                {
                    answer.QuestionId,
                    IsFailed = selectedOption?.IsCorrect != true
                };
            })
            .Where(x => x.IsFailed)
            .GroupBy(x => x.QuestionId)
            .Select(group => new
            {
                QuestionId = group.Key,
                FailedAnswersCount = group.Count()
            })
            .OrderByDescending(x => x.FailedAnswersCount)
            .Take(10)
            .ToList();

        var mostFailedQuestions = failedQuestionCounts
            .Select(failed =>
            {
                var question = questions.First(x => x.Id == failed.QuestionId);
                var quiz = quizzes.FirstOrDefault(x => x.Id == question.QuizId);

                return new FailedQuestionViewModel(
                    question.Id,
                    question.Text,
                    question.QuizId,
                    quiz?.Title ?? "Deleted quiz",
                    failed.FailedAnswersCount);
            })
            .ToList();

        var passedAttempts = completedAttempts.Count(x => x.IsPassed);
        var viewModel = new PerformanceAnalyticsViewModel(
            attempts.Count,
            completedAttempts.Count,
            passedAttempts,
            completedAttempts.Count == 0
                ? 0
                : Math.Round((decimal)passedAttempts / completedAttempts.Count * 100, 2),
            passRateByQuiz,
            averageScoreByDiploma,
            attemptsOverTime,
            mostFailedQuestions);

        return Task.FromResult(Result<PerformanceAnalyticsViewModel>.Success(viewModel));
    }
}
