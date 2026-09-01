using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetQuizTimerOrchestrator : IGetQuizTimerOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public GetQuizTimerOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<QuizTimerViewModel>> GetAsync(
        GetQuizTimerQuery query,
        CancellationToken cancellationToken = default)
    {
        var attempt = await _unitOfWork.Repository<QuizAttempt>()
            .FirstOrDefaultAsync(
                x => x.Id == query.AttemptId && x.StudentId == query.StudentId,
                cancellationToken);

        if (attempt is null)
        {
            return Result<QuizTimerViewModel>.Failure("Attempt was not found.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .FirstOrDefaultAsync(x => x.Id == attempt.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<QuizTimerViewModel>.Failure("Quiz was not found.");
        }

        var serverNow = DateTime.UtcNow;
        var endsAt = attempt.StartTime.AddMinutes(quiz.Duration);

        if (attempt.Status == AttemptStatus.InProgress && serverNow >= endsAt)
        {
            await ExpireAttemptAsync(attempt, quiz, serverNow, cancellationToken);
        }

        var remainingSeconds = attempt.Status == AttemptStatus.InProgress
            ? Math.Max(0, (int)(endsAt - serverNow).TotalSeconds)
            : 0;

        var viewModel = new QuizTimerViewModel(
            attempt.Id,
            attempt.QuizId,
            serverNow,
            attempt.StartTime,
            endsAt,
            remainingSeconds,
            attempt.Status.ToString(),
            attempt.Status != AttemptStatus.InProgress);

        return Result<QuizTimerViewModel>.Success(viewModel);
    }

    private async Task ExpireAttemptAsync(
        QuizAttempt attempt,
        QuizEntity quiz,
        DateTime expiredAt,
        CancellationToken cancellationToken)
    {
        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => x.QuizId == attempt.QuizId)
            .ToList();

        var questionIds = questions.Select(x => x.Id).ToList();
        var answers = _unitOfWork.Repository<AttemptAnswer>()
            .Query()
            .Where(x => x.AttemptId == attempt.Id && questionIds.Contains(x.QuestionId))
            .ToList();

        var optionIds = answers
            .Where(x => x.SelectedOptionId.HasValue)
            .Select(x => x.SelectedOptionId!.Value)
            .ToList();

        var selectedOptions = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => optionIds.Contains(x.Id))
            .ToList();

        var score = questions.Sum(question =>
        {
            var answer = answers.FirstOrDefault(x => x.QuestionId == question.Id);
            if (answer?.SelectedOptionId is null)
            {
                return 0;
            }

            var selectedOption = selectedOptions.FirstOrDefault(x => x.Id == answer.SelectedOptionId.Value);

            return selectedOption?.IsCorrect == true ? question.Score : 0;
        });

        attempt.Score = score;
        attempt.IsPassed = score >= quiz.PassScore;
        attempt.Status = AttemptStatus.Expired;
        attempt.EndTime = expiredAt;

        _unitOfWork.Repository<QuizAttempt>().Update(attempt);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizTimerExpiredEvent(
                attempt.Id,
                attempt.QuizId,
                attempt.StudentId,
                attempt.Score,
                expiredAt),
            cancellationToken);
    }
}
