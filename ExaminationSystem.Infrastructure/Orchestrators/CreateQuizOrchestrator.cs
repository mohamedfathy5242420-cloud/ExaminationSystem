using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class CreateQuizOrchestrator : ICreateQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<CreateQuizViewModel>> CreateAsync(
        CreateQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var diplomaExists = await _unitOfWork.Repository<Diploma>()
            .AnyAsync(x => x.Id == command.DiplomaId, cancellationToken);

        if (!diplomaExists)
        {
            return Result<CreateQuizViewModel>.Failure("Diploma was not found.");
        }

        var title = command.Title.Trim();
        var titleExists = await _unitOfWork.Repository<QuizEntity>()
            .AnyAsync(x => x.DiplomaId == command.DiplomaId && x.Title == title, cancellationToken);

        if (titleExists)
        {
            return Result<CreateQuizViewModel>.Failure("Quiz title already exists in this diploma.");
        }

        var quiz = new QuizEntity
        {
            DiplomaId = command.DiplomaId,
            Title = title,
            Duration = command.Duration,
            PassScore = command.PassScore,
            MaxAttempts = command.MaxAttempts,
            Instructions = command.Instructions.Trim(),
            IsPublished = false
        };

        await _unitOfWork.Repository<QuizEntity>().AddAsync(quiz, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizCreatedEvent(quiz.Id, quiz.DiplomaId, quiz.Title),
            cancellationToken);

        var viewModel = new CreateQuizViewModel(
            quiz.Id,
            quiz.DiplomaId,
            quiz.Title,
            quiz.Duration,
            quiz.PassScore,
            quiz.MaxAttempts,
            quiz.Instructions,
            quiz.IsPublished);

        return Result<CreateQuizViewModel>.Success(viewModel);
    }
}
