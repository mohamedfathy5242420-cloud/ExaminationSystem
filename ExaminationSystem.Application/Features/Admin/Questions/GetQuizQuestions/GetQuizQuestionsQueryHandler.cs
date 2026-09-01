using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Domain.Entities.Quiz;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions;

public sealed class GetQuizQuestionsQueryHandler
    : IRequestHandler<GetQuizQuestionsQuery, Result<IReadOnlyList<QuestionListItemViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetQuizQuestionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IReadOnlyList<QuestionListItemViewModel>>> Handle(
        GetQuizQuestionsQuery query,
        CancellationToken cancellationToken)
    {
        var questions = _unitOfWork.Repository<Question>()
            .Query()
            .Where(x => x.QuizId == query.QuizId)
            .OrderBy(x => x.Order)
            .ToList();

        var questionIds = questions.Select(x => x.Id).ToList();
        var options = _unitOfWork.Repository<QuestionOption>()
            .Query()
            .Where(x => questionIds.Contains(x.QuestionId))
            .ToList();

        var viewModels = questions
            .Select(question => new QuestionListItemViewModel(
                question.Id,
                question.QuizId,
                question.Text,
                question.Explanation,
                question.Order,
                question.Score,
                options
                    .Where(option => option.QuestionId == question.Id)
                    .Select(option => new QuestionOptionViewModel(
                        option.Id,
                        option.Text,
                        option.IsCorrect))
                    .ToList()))
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<QuestionListItemViewModel>>.Success(viewModels));
    }
}
