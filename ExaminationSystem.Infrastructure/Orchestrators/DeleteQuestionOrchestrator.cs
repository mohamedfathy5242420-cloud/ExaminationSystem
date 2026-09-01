using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class DeleteQuestionOrchestrator : IDeleteQuestionOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public DeleteQuestionOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<DeleteQuestionViewModel>> DeleteAsync(
        DeleteQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var question = await _unitOfWork.Repository<Question>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (question is null)
        {
            return Result<DeleteQuestionViewModel>.Failure("Question was not found.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(question.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<DeleteQuestionViewModel>.Failure("Quiz was not found.");
        }

        if (quiz.IsPublished)
        {
            return Result<DeleteQuestionViewModel>.Failure("Cannot delete questions from a published quiz.");
        }

        var options = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => x.QuestionId == question.Id)
            .ToList();

        foreach (var option in options)
        {
            _unitOfWork.Repository<QuestionOption>().Delete(option);
        }

        _unitOfWork.Repository<Question>().Delete(question);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuestionDeletedEvent(question.Id, question.QuizId, question.Text),
            cancellationToken);

        return Result<DeleteQuestionViewModel>.Success(
            new DeleteQuestionViewModel(question.Id, "Question deleted successfully."));
    }
}
