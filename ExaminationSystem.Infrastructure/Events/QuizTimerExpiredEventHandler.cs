using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizTimerExpiredEventHandler : IEventHandler<QuizTimerExpiredEvent>
{
    private readonly ILogger<QuizTimerExpiredEventHandler> _logger;

    public QuizTimerExpiredEventHandler(ILogger<QuizTimerExpiredEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizTimerExpiredEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz attempt expired: {AttemptId}, quiz {QuizId}, student {StudentId}, score {Score}.",
            domainEvent.AttemptId,
            domainEvent.QuizId,
            domainEvent.StudentId,
            domainEvent.Score);

        return Task.CompletedTask;
    }
}
