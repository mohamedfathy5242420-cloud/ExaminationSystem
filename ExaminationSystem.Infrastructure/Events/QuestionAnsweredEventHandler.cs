using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuestionAnsweredEventHandler : IEventHandler<QuestionAnsweredEvent>
{
    private readonly ILogger<QuestionAnsweredEventHandler> _logger;

    public QuestionAnsweredEventHandler(ILogger<QuestionAnsweredEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuestionAnsweredEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Question answered: attempt {AttemptId}, question {QuestionId}, student {StudentId}.",
            domainEvent.AttemptId,
            domainEvent.QuestionId,
            domainEvent.StudentId);

        return Task.CompletedTask;
    }
}
