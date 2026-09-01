using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizUpdatedEventHandler : IEventHandler<QuizUpdatedEvent>
{
    private readonly ILogger<QuizUpdatedEventHandler> _logger;

    public QuizUpdatedEventHandler(ILogger<QuizUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizUpdatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz updated: {QuizId}, {Title}, diploma {DiplomaId}.",
            domainEvent.QuizId,
            domainEvent.Title,
            domainEvent.DiplomaId);

        return Task.CompletedTask;
    }
}
