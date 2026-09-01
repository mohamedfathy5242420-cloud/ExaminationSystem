using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class PublishQuizOrchestrator : IPublishQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public PublishQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<PublishQuizViewModel>> PublishAsync(
        PublishQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (quiz is null)
        {
            return Result<PublishQuizViewModel>.Failure("Quiz was not found.");
        }

        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => x.QuizId == quiz.Id)
            .ToList();

        if (questions.Count == 0)
        {
            return Result<PublishQuizViewModel>.Failure("Quiz must contain at least one question before publishing.");
        }

        var questionIds = questions.Select(x => x.Id).ToList();
        var options = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => questionIds.Contains(x.QuestionId))
            .ToList();

        var invalidQuestionExists = questions.Any(question =>
        {
            var questionOptions = options.Where(option => option.QuestionId == question.Id).ToList();

            return questionOptions.Count < 2 || questionOptions.Count(option => option.IsCorrect) != 1;
        });

        if (invalidQuestionExists)
        {
            return Result<PublishQuizViewModel>.Failure(
                "Every question must have at least two options and exactly one correct answer.");
        }

        quiz.IsPublished = true;

        _unitOfWork.Repository<QuizEntity>().Update(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizPublishedEvent(quiz.Id, quiz.Title),
            cancellationToken);

        return Result<PublishQuizViewModel>.Success(
            new PublishQuizViewModel(quiz.Id, quiz.Title, quiz.IsPublished, "Quiz published successfully."));
    }
}
