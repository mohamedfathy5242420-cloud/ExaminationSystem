using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class UpdateQuestionOrchestrator : IUpdateQuestionOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public UpdateQuestionOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<UpdateQuestionViewModel>> UpdateAsync(
        UpdateQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var question = await _unitOfWork.Repository<Question>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (question is null)
        {
            return Result<UpdateQuestionViewModel>.Failure("Question was not found.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(question.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<UpdateQuestionViewModel>.Failure("Quiz was not found.");
        }

        if (quiz.IsPublished)
        {
            return Result<UpdateQuestionViewModel>.Failure("Cannot update questions in a published quiz.");
        }

        var orderExists = await _unitOfWork.Repository<Question>()
            .AnyAsync(
                x => x.Id != question.Id
                    && x.QuizId == question.QuizId
                    && x.Order == command.Order,
                cancellationToken);

        if (orderExists)
        {
            return Result<UpdateQuestionViewModel>.Failure("Question order already exists in this quiz.");
        }

        var oldOptions = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => x.QuestionId == question.Id)
            .ToList();

        foreach (var option in oldOptions)
        {
            _unitOfWork.Repository<QuestionOption>().Delete(option);
        }

        var newOptions = command.Options
            .Select(option => new QuestionOption
            {
                QuestionId = question.Id,
                Text = option.Text.Trim(),
                IsCorrect = option.IsCorrect
            })
            .ToList();

        foreach (var option in newOptions)
        {
            await _unitOfWork.Repository<QuestionOption>().AddAsync(option, cancellationToken);
        }

        question.Text = command.Text.Trim();
        question.Explanation = command.Explanation.Trim();
        question.Order = command.Order;
        question.Score = command.Score;

        _unitOfWork.Repository<Question>().Update(question);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuestionUpdatedEvent(question.Id, question.QuizId, question.Text),
            cancellationToken);

        var viewModel = new UpdateQuestionViewModel(
            question.Id,
            question.QuizId,
            question.Text,
            question.Explanation,
            question.Order,
            question.Score,
            newOptions
                .Select(option => new UpdateQuestionOptionViewModel(
                    option.Id,
                    option.Text,
                    option.IsCorrect))
                .ToList());

        return Result<UpdateQuestionViewModel>.Success(viewModel);
    }
}
