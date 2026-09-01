using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Learning;
using ExaminationSystem.Domain.Entities.Quiz;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetInstructorDiplomaQuizzesOrchestrator : IGetInstructorDiplomaQuizzesOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInstructorDiplomaQuizzesOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<InstructorQuizListItemViewModel>>> GetAsync(
        GetInstructorDiplomaQuizzesQuery query,
        CancellationToken cancellationToken = default)
    {
        var diplomaExists = _unitOfWork.Repository<Diploma>()
            .Query()
            .Any(x => x.Id == query.DiplomaId && x.InstructorId == query.InstructorId);

        if (!diplomaExists)
        {
            return Task.FromResult(
                Result<IReadOnlyList<InstructorQuizListItemViewModel>>.Failure("Diploma was not found."));
        }

        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => x.DiplomaId == query.DiplomaId)
            .OrderBy(x => x.Title)
            .ToList();

        var quizIds = quizzes.Select(x => x.Id).ToList();
        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => quizIds.Contains(x.QuizId))
            .ToList();

        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => quizIds.Contains(x.QuizId))
            .ToList();

        var viewModels = quizzes
            .Select(quiz => new InstructorQuizListItemViewModel(
                quiz.Id,
                quiz.DiplomaId,
                quiz.Title,
                quiz.Duration,
                quiz.PassScore,
                quiz.MaxAttempts,
                quiz.Instructions,
                quiz.IsPublished,
                questions.Count(x => x.QuizId == quiz.Id),
                attempts.Count(x => x.QuizId == quiz.Id)))
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<InstructorQuizListItemViewModel>>.Success(viewModels));
    }
}
