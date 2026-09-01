using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;

public sealed class PublishQuizCommandHandler
    : IRequestHandler<PublishQuizCommand, Result<PublishQuizViewModel>>
{
    private readonly IPublishQuizOrchestrator _publishQuizOrchestrator;

    public PublishQuizCommandHandler(IPublishQuizOrchestrator publishQuizOrchestrator)
    {
        _publishQuizOrchestrator = publishQuizOrchestrator;
    }

    public Task<Result<PublishQuizViewModel>> Handle(
        PublishQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _publishQuizOrchestrator.PublishAsync(command, cancellationToken);
    }
}
