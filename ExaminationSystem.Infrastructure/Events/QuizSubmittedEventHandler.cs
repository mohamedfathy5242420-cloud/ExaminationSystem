using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuizSubmittedEventHandler : IEventHandler<QuizSubmittedEvent>
{
    private readonly ILogger<QuizSubmittedEventHandler> _logger;

    public QuizSubmittedEventHandler(ILogger<QuizSubmittedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuizSubmittedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Quiz submitted: attempt {AttemptId}, quiz {QuizId}, student {StudentId}, score {Score}.",
            domainEvent.AttemptId,
            domainEvent.QuizId,
            domainEvent.StudentId,
            domainEvent.Score);

        return Task.CompletedTask;
    }
}
