using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class UnpublishQuizOrchestrator : IUnpublishQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public UnpublishQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<PublishQuizViewModel>> UnpublishAsync(
        UnpublishQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (quiz is null)
        {
            return Result<PublishQuizViewModel>.Failure("Quiz was not found.");
        }

        quiz.IsPublished = false;

        _unitOfWork.Repository<QuizEntity>().Update(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizUnpublishedEvent(quiz.Id, quiz.Title),
            cancellationToken);

        return Result<PublishQuizViewModel>.Success(
            new PublishQuizViewModel(quiz.Id, quiz.Title, quiz.IsPublished, "Quiz unpublished successfully."));
    }
}
