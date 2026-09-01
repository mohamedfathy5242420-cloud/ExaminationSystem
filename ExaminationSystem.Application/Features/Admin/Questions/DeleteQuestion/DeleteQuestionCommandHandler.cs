using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;

public sealed class DeleteQuestionCommandHandler
    : IRequestHandler<DeleteQuestionCommand, Result<DeleteQuestionViewModel>>
{
    private readonly IDeleteQuestionOrchestrator _deleteQuestionOrchestrator;

    public DeleteQuestionCommandHandler(IDeleteQuestionOrchestrator deleteQuestionOrchestrator)
    {
        _deleteQuestionOrchestrator = deleteQuestionOrchestrator;
    }

    public Task<Result<DeleteQuestionViewModel>> Handle(
        DeleteQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return _deleteQuestionOrchestrator.DeleteAsync(command, cancellationToken);
    }
}
