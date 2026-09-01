using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;

public sealed class UpdateQuestionCommandHandler
    : IRequestHandler<UpdateQuestionCommand, Result<UpdateQuestionViewModel>>
{
    private readonly IUpdateQuestionOrchestrator _updateQuestionOrchestrator;

    public UpdateQuestionCommandHandler(IUpdateQuestionOrchestrator updateQuestionOrchestrator)
    {
        _updateQuestionOrchestrator = updateQuestionOrchestrator;
    }

    public Task<Result<UpdateQuestionViewModel>> Handle(
        UpdateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return _updateQuestionOrchestrator.UpdateAsync(command, cancellationToken);
    }
}
