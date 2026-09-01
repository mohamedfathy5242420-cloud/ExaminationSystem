using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;

public sealed class GetQuizTimerQueryHandler
    : IRequestHandler<GetQuizTimerQuery, Result<QuizTimerViewModel>>
{
    private readonly IGetQuizTimerOrchestrator _getQuizTimerOrchestrator;

    public GetQuizTimerQueryHandler(IGetQuizTimerOrchestrator getQuizTimerOrchestrator)
    {
        _getQuizTimerOrchestrator = getQuizTimerOrchestrator;
    }

    public Task<Result<QuizTimerViewModel>> Handle(
        GetQuizTimerQuery query,
        CancellationToken cancellationToken)
    {
        return _getQuizTimerOrchestrator.GetAsync(query, cancellationToken);
    }
}
