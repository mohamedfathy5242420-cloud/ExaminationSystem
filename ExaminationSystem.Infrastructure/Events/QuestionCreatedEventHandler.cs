using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuestionCreatedEventHandler : IEventHandler<QuestionCreatedEvent>
{
    private readonly ILogger<QuestionCreatedEventHandler> _logger;

    public QuestionCreatedEventHandler(ILogger<QuestionCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuestionCreatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Question created: {QuestionId}, quiz {QuizId}, {Text}.",
            domainEvent.QuestionId,
            domainEvent.QuizId,
            domainEvent.Text);

        return Task.CompletedTask;
    }
}
