using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;

public sealed class DeleteQuizCommandHandler
    : IRequestHandler<DeleteQuizCommand, Result<DeleteQuizViewModel>>
{
    private readonly IDeleteQuizOrchestrator _deleteQuizOrchestrator;

    public DeleteQuizCommandHandler(IDeleteQuizOrchestrator deleteQuizOrchestrator)
    {
        _deleteQuizOrchestrator = deleteQuizOrchestrator;
    }

    public Task<Result<DeleteQuizViewModel>> Handle(
        DeleteQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _deleteQuizOrchestrator.DeleteAsync(command, cancellationToken);
    }
}
