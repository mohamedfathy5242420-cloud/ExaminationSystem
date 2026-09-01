using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory;

public sealed class GetQuizHistoryQueryHandler
    : IRequestHandler<GetQuizHistoryQuery, Result<IReadOnlyList<QuizHistoryItemViewModel>>>
{
    private readonly IGetQuizHistoryOrchestrator _getQuizHistoryOrchestrator;

    public GetQuizHistoryQueryHandler(IGetQuizHistoryOrchestrator getQuizHistoryOrchestrator)
    {
        _getQuizHistoryOrchestrator = getQuizHistoryOrchestrator;
    }

    public Task<Result<IReadOnlyList<QuizHistoryItemViewModel>>> Handle(
        GetQuizHistoryQuery query,
        CancellationToken cancellationToken)
    {
        return _getQuizHistoryOrchestrator.GetAsync(query, cancellationToken);
    }
}
