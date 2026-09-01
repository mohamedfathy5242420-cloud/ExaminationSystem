using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult;

public sealed class GetQuizResultQueryHandler
    : IRequestHandler<GetQuizResultQuery, Result<QuizResultViewModel>>
{
    private readonly IGetQuizResultOrchestrator _getQuizResultOrchestrator;

    public GetQuizResultQueryHandler(IGetQuizResultOrchestrator getQuizResultOrchestrator)
    {
        _getQuizResultOrchestrator = getQuizResultOrchestrator;
    }

    public Task<Result<QuizResultViewModel>> Handle(
        GetQuizResultQuery query,
        CancellationToken cancellationToken)
    {
        return _getQuizResultOrchestrator.GetAsync(query, cancellationToken);
    }
}
