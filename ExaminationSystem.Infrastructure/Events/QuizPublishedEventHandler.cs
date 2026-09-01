using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizPublishedEventHandler : IEventHandler<QuizPublishedEvent>
{
    private readonly ILogger<QuizPublishedEventHandler> _logger;

    public QuizPublishedEventHandler(ILogger<QuizPublishedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizPublishedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz published: {QuizId}, {Title}.",
            domainEvent.QuizId,
            domainEvent.Title);

        return Task.CompletedTask;
    }
}
