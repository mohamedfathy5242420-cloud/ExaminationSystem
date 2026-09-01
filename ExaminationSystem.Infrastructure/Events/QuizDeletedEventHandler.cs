using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizDeletedEventHandler : IEventHandler<QuizDeletedEvent>
{
    private readonly ILogger<QuizDeletedEventHandler> _logger;

    public QuizDeletedEventHandler(ILogger<QuizDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizDeletedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz deleted: {QuizId}, {Title}.",
            domainEvent.QuizId,
            domainEvent.Title);

        return Task.CompletedTask;
    }
}
