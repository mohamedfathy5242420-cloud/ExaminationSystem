using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class SubmitQuizOrchestrator : ISubmitQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public SubmitQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<SubmitQuizViewModel>> SubmitAsync(
        SubmitQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var attempt = await _unitOfWork.Repository<QuizAttempt>()
            .FirstOrDefaultAsync(
                x => x.Id == command.AttemptId && x.StudentId == command.StudentId,
                cancellationToken);

        if (attempt is null)
        {
            return Result<SubmitQuizViewModel>.Failure("Attempt was not found.");
        }

        if (attempt.Status != AttemptStatus.InProgress)
        {
            return Result<SubmitQuizViewModel>.Failure("Attempt is already closed.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .FirstOrDefaultAsync(x => x.Id == attempt.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<SubmitQuizViewModel>.Failure("Quiz was not found.");
        }

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

        var totalScore = questions.Sum(x => x.Score);
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

        var submittedAt = DateTime.UtcNow;
        var isExpired = submittedAt > attempt.StartTime.AddMinutes(quiz.Duration);

        attempt.Score = score;
        attempt.IsPassed = score >= quiz.PassScore;
        attempt.Status = isExpired ? AttemptStatus.Expired : AttemptStatus.Submitted;
        attempt.EndTime = submittedAt;

        _unitOfWork.Repository<QuizAttempt>().Update(attempt);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizSubmittedEvent(
                attempt.Id,
                attempt.QuizId,
                attempt.StudentId,
                attempt.Score,
                attempt.IsPassed),
            cancellationToken);

        var viewModel = new SubmitQuizViewModel(
            attempt.Id,
            attempt.QuizId,
            attempt.Score,
            totalScore,
            quiz.PassScore,
            attempt.IsPassed,
            attempt.Status.ToString(),
            submittedAt);

        return Result<SubmitQuizViewModel>.Success(viewModel);
    }
}
