using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Quiz;
using Microsoft.AspNetCore.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class StartQuizOrchestrator : IStartQuizOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly UserManager<User> _userManager;

    public StartQuizOrchestrator(
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
        _userManager = userManager;
    }

    public async Task<Result<StartQuizViewModel>> StartAsync(
        StartQuizCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.StudentId.ToString());

        if (user is not Student)
        {
            return Result<StartQuizViewModel>.Failure("Student was not found.");
        }

        var quiz = await _unitOfWork.Repository<QuizEntity>()
            .FirstOrDefaultAsync(x => x.Id == command.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<StartQuizViewModel>.Failure("Quiz was not found.");
        }

        if (!quiz.IsPublished)
        {
            return Result<StartQuizViewModel>.Failure("Quiz is not published.");
        }

        var hasOpenAttempt = await _unitOfWork.Repository<QuizAttempt>()
            .AnyAsync(
                x => x.StudentId == command.StudentId
                    && x.QuizId == command.QuizId
                    && x.Status == AttemptStatus.InProgress,
                cancellationToken);

        if (hasOpenAttempt)
        {
            return Result<StartQuizViewModel>.Failure("Student already has an open attempt for this quiz.");
        }

        var previousAttemptsCount = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Count(x => x.StudentId == command.StudentId && x.QuizId == command.QuizId);

        if (previousAttemptsCount >= quiz.MaxAttempts)
        {
            return Result<StartQuizViewModel>.Failure("Student exceeded the maximum attempts for this quiz.");
        }

        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => x.QuizId == command.QuizId)
            .ToList();

        if (questions.Count == 0)
        {
            return Result<StartQuizViewModel>.Failure("Quiz has no questions.");
        }

        var questionIds = questions.Select(x => x.Id).ToList();
        var options = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => questionIds.Contains(x.QuestionId))
            .ToList();

        var startedAt = DateTime.UtcNow;
        var attempt = new QuizAttempt
        {
            QuizId = command.QuizId,
            StudentId = command.StudentId,
            StartTime = startedAt,
            Status = AttemptStatus.InProgress,
            Score = 0,
            IsPassed = false,
            Answers = questions
                .Select(question => new AttemptAnswer
                {
                    QuestionId = question.Id
                })
                .ToList()
        };

        await _unitOfWork.Repository<QuizAttempt>().AddAsync(attempt, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new QuizStartedEvent(attempt.Id, attempt.QuizId, attempt.StudentId, attempt.StartTime),
            cancellationToken);

        var randomizedQuestions = questions
            .OrderBy(_ => Guid.NewGuid())
            .Select(question => new StartQuizQuestionViewModel(
                question.Id,
                question.Text,
                question.Score,
                options
                    .Where(option => option.QuestionId == question.Id)
                    .OrderBy(_ => Guid.NewGuid())
                    .Select(option => new StartQuizOptionViewModel(
                        option.Id,
                        option.Text))
                    .ToList()))
            .ToList();

        var endsAt = startedAt.AddMinutes(quiz.Duration);
        var viewModel = new StartQuizViewModel(
            attempt.Id,
            quiz.Id,
            quiz.Title,
            attempt.StartTime,
            endsAt,
            quiz.Duration,
            (int)(endsAt - DateTime.UtcNow).TotalSeconds,
            randomizedQuestions);

        return Result<StartQuizViewModel>.Success(viewModel);
    }
}
