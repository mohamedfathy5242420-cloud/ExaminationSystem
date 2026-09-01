using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class AnswerQuestionOrchestrator : IAnswerQuestionOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public AnswerQuestionOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<AnswerQuestionViewModel>> AnswerAsync(
        AnswerQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var attempt = await _unitOfWork.Repository<QuizAttempt>()
            .FirstOrDefaultAsync(
                x => x.Id == command.AttemptId && x.StudentId == command.StudentId,
                cancellationToken);

        if (attempt is null)
        {
            return Result<AnswerQuestionViewModel>.Failure("Attempt was not found.");
        }

        if (attempt.Status != AttemptStatus.InProgress)
        {
            return Result<AnswerQuestionViewModel>.Failure("Attempt is not open.");
        }

        var quiz = await _unitOfWork.Repository<Quiz>()
            .FirstOrDefaultAsync(x => x.Id == attempt.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<AnswerQuestionViewModel>.Failure("Quiz was not found.");
        }

        var now = DateTime.UtcNow;
        if (now > attempt.StartTime.AddMinutes(quiz.Duration))
        {
            attempt.Status = AttemptStatus.Expired;
            attempt.EndTime = now;
            _unitOfWork.Repository<QuizAttempt>().Update(attempt);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<AnswerQuestionViewModel>.Failure("Attempt time has expired.");
        }

        var questionExists = await _unitOfWork.Repository<Question>()
            .AnyAsync(
                x => x.Id == command.QuestionId && x.QuizId == attempt.QuizId,
                cancellationToken);

        if (!questionExists)
        {
            return Result<AnswerQuestionViewModel>.Failure("Question does not belong to this attempt.");
        }

        var optionExists = await _unitOfWork.Repository<QuestionOption>()
            .AnyAsync(
                x => x.Id == command.SelectedOptionId && x.QuestionId == command.QuestionId,
                cancellationToken);

        if (!optionExists)
        {
            return Result<AnswerQuestionViewModel>.Failure("Selected option does not belong to this question.");
        }

        var answer = await _unitOfWork.Repository<AttemptAnswer>()
            .FirstOrDefaultAsync(
                x => x.AttemptId == command.AttemptId && x.QuestionId == command.QuestionId,
                cancellationToken);

        if (answer is null)
        {
            answer = new AttemptAnswer
            {
                AttemptId = command.AttemptId,
                QuestionId = command.QuestionId
            };

            await _unitOfWork.Repository<AttemptAnswer>().AddAsync(answer, cancellationToken);
        }

        answer.SelectedOptionId = command.SelectedOptionId;
        answer.AnswerText = null;
        _unitOfWork.Repository<AttemptAnswer>().Update(answer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuestionAnsweredEvent(
                command.AttemptId,
                command.QuestionId,
                command.SelectedOptionId,
                command.StudentId),
            cancellationToken);

        var viewModel = new AnswerQuestionViewModel(
            command.AttemptId,
            command.QuestionId,
            command.SelectedOptionId,
            now);

        return Result<AnswerQuestionViewModel>.Success(viewModel);
    }
}
