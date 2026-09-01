using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes.ViewModels;
using ExaminationSystem.Application.Interfaces;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes;

public sealed class GetDiplomaQuizzesQueryHandler
    : IRequestHandler<GetDiplomaQuizzesQuery, Result<IReadOnlyList<QuizListItemViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDiplomaQuizzesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<QuizListItemViewModel>>> Handle(
        GetDiplomaQuizzesQuery query,
        CancellationToken cancellationToken)
    {
        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => x.DiplomaId == query.DiplomaId)
            .OrderByDescending(x => x.CreatedOnUtc)
            .Select(x => new QuizListItemViewModel(
                x.Id,
                x.DiplomaId,
                x.Title,
                x.Duration,
                x.PassScore,
                x.MaxAttempts,
                x.Instructions,
                x.IsPublished,
                x.CreatedOnUtc))
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<QuizListItemViewModel>>.Success(quizzes));
    }
}
