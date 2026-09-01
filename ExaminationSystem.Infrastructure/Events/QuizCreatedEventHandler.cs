using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizCreatedEventHandler : IEventHandler<QuizCreatedEvent>
{
    private readonly ILogger<QuizCreatedEventHandler> _logger;

    public QuizCreatedEventHandler(ILogger<QuizCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizCreatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz created: {QuizId}, {Title}, diploma {DiplomaId}.",
            domainEvent.QuizId,
            domainEvent.Title,
            domainEvent.DiplomaId);

        return Task.CompletedTask;
    }
}
