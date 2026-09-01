using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;

public sealed class StartQuizCommandHandler
    : IRequestHandler<StartQuizCommand, Result<StartQuizViewModel>>
{
    private readonly IStartQuizOrchestrator _startQuizOrchestrator;

    public StartQuizCommandHandler(IStartQuizOrchestrator startQuizOrchestrator)
    {
        _startQuizOrchestrator = startQuizOrchestrator;
    }

    public Task<Result<StartQuizViewModel>> Handle(
        StartQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _startQuizOrchestrator.StartAsync(command, cancellationToken);
    }
}
