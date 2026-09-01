using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class DeleteQuizOrchestrator : IDeleteQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public DeleteQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<DeleteQuizViewModel>> DeleteAsync(
        DeleteQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (quiz is null)
        {
            return Result<DeleteQuizViewModel>.Failure("Quiz was not found.");
        }

        _unitOfWork.Repository<QuizEntity>().Delete(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizDeletedEvent(quiz.Id, quiz.Title),
            cancellationToken);

        return Result<DeleteQuizViewModel>.Success(
            new DeleteQuizViewModel(quiz.Id, "Quiz deleted successfully."));
    }
}
