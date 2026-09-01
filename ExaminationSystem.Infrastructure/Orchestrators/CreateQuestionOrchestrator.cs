using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class CreateQuestionOrchestrator : ICreateQuestionOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateQuestionOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<CreateQuestionViewModel>> CreateAsync(
        CreateQuestionCommand command,
        CancellationToken cancellationToken = default)
    {
        var quiz = _unitOfWork.Repository<Quiz>()
            .Query()
            .FirstOrDefault(x => x.Id == command.QuizId);

        if (quiz is null)
        {
            return Result<CreateQuestionViewModel>.Failure("Quiz was not found.");
        }

        if (quiz.IsPublished)
        {
            return Result<CreateQuestionViewModel>.Failure("Cannot add questions to a published quiz.");
        }

        var orderExists = await _unitOfWork.Repository<Question>()
            .AnyAsync(
                x => x.QuizId == command.QuizId && x.Order == command.Order,
                cancellationToken);

        if (orderExists)
        {
            return Result<CreateQuestionViewModel>.Failure("Question order already exists in this quiz.");
        }

        var question = new Question
        {
            QuizId = command.QuizId,
            Text = command.Text.Trim(),
            Explanation = command.Explanation.Trim(),
            Order = command.Order,
            Score = command.Score,
            Options = command.Options
                .Select(option => new QuestionOption
                {
                    Text = option.Text.Trim(),
                    IsCorrect = option.IsCorrect
                })
                .ToList()
        };

        await _unitOfWork.Repository<Question>().AddAsync(question, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuestionCreatedEvent(question.Id, question.QuizId, question.Text),
            cancellationToken);

        var viewModel = new CreateQuestionViewModel(
            question.Id,
            question.QuizId,
            question.Text,
            question.Explanation,
            question.Order,
            question.Score,
            question.Options
                .Select(option => new CreateQuestionOptionViewModel(
                    option.Id,
                    option.Text,
                    option.IsCorrect))
                .ToList());

        return Result<CreateQuestionViewModel>.Success(viewModel);
    }
}
