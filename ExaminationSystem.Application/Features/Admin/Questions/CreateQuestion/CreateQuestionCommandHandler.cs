using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;

public sealed class CreateQuestionCommandHandler
    : IRequestHandler<CreateQuestionCommand, Result<CreateQuestionViewModel>>
{
    private readonly ICreateQuestionOrchestrator _createQuestionOrchestrator;

    public CreateQuestionCommandHandler(ICreateQuestionOrchestrator createQuestionOrchestrator)
    {
        _createQuestionOrchestrator = createQuestionOrchestrator;
    }

    public Task<Result<CreateQuestionViewModel>> Handle(
        CreateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return _createQuestionOrchestrator.CreateAsync(command, cancellationToken);
    }
}
