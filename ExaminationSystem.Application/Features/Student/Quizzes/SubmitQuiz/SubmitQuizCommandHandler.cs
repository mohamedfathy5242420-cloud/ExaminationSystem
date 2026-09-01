using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;

public sealed class SubmitQuizCommandHandler
    : IRequestHandler<SubmitQuizCommand, Result<SubmitQuizViewModel>>
{
    private readonly ISubmitQuizOrchestrator _submitQuizOrchestrator;

    public SubmitQuizCommandHandler(ISubmitQuizOrchestrator submitQuizOrchestrator)
    {
        _submitQuizOrchestrator = submitQuizOrchestrator;
    }

    public Task<Result<SubmitQuizViewModel>> Handle(
        SubmitQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _submitQuizOrchestrator.SubmitAsync(command, cancellationToken);
    }
}
