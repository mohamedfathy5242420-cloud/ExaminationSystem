using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class UpdateQuizOrchestrator : IUpdateQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public UpdateQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<UpdateQuizViewModel>> UpdateAsync(
        UpdateQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .GetByIdAsync(command.Id, cancellationToken);

        if (quiz is null)
        {
            return Result<UpdateQuizViewModel>.Failure("Quiz was not found.");
        }

        var title = command.Title.Trim();
        var titleExists = await _unitOfWork.Repository<QuizEntity>()
            .AnyAsync(x => x.Id != quiz.Id &&
                           x.DiplomaId == quiz.DiplomaId &&
                           x.Title == title,
                cancellationToken);

        if (titleExists)
        {
            return Result<UpdateQuizViewModel>.Failure("Quiz title already exists in this diploma.");
        }

        quiz.Title = title;
        quiz.Duration = command.Duration;
        quiz.PassScore = command.PassScore;
        quiz.MaxAttempts = command.MaxAttempts;
        quiz.Instructions = command.Instructions.Trim();

        _unitOfWork.Repository<QuizEntity>().Update(quiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizUpdatedEvent(quiz.Id, quiz.DiplomaId, quiz.Title),
            cancellationToken);

        var viewModel = new UpdateQuizViewModel(
            quiz.Id,
            quiz.DiplomaId,
            quiz.Title,
            quiz.Duration,
            quiz.PassScore,
            quiz.MaxAttempts,
            quiz.Instructions,
            quiz.IsPublished);

        return Result<UpdateQuizViewModel>.Success(viewModel);
    }
}
