using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizStartedEventHandler : IEventHandler<QuizStartedEvent>
{
    private readonly ILogger<QuizStartedEventHandler> _logger;

    public QuizStartedEventHandler(ILogger<QuizStartedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizStartedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz attempt started: {AttemptId}, quiz {QuizId}, student {StudentId}.",
            domainEvent.AttemptId,
            domainEvent.QuizId,
            domainEvent.StudentId);

        return Task.CompletedTask;
    }
}
