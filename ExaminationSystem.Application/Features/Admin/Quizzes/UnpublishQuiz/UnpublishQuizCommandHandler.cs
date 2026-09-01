using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;

public sealed class UnpublishQuizCommandHandler
    : IRequestHandler<UnpublishQuizCommand, Result<PublishQuizViewModel>>
{
    private readonly IUnpublishQuizOrchestrator _unpublishQuizOrchestrator;

    public UnpublishQuizCommandHandler(IUnpublishQuizOrchestrator unpublishQuizOrchestrator)
    {
        _unpublishQuizOrchestrator = unpublishQuizOrchestrator;
    }

    public Task<Result<PublishQuizViewModel>> Handle(
        UnpublishQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _unpublishQuizOrchestrator.UnpublishAsync(command, cancellationToken);
    }
}
