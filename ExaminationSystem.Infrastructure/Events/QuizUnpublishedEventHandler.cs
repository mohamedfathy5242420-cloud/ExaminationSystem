using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizUnpublishedEventHandler : IEventHandler<QuizUnpublishedEvent>
{
    private readonly ILogger<QuizUnpublishedEventHandler> _logger;

    public QuizUnpublishedEventHandler(ILogger<QuizUnpublishedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizUnpublishedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz unpublished: {QuizId}, {Title}.",
            domainEvent.QuizId,
            domainEvent.Title);

        return Task.CompletedTask;
    }
}
