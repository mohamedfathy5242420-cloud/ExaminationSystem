using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuestionUpdatedEventHandler : IEventHandler<QuestionUpdatedEvent>
{
    private readonly ILogger<QuestionUpdatedEventHandler> _logger;

    public QuestionUpdatedEventHandler(ILogger<QuestionUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuestionUpdatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Question updated: {QuestionId}, quiz {QuizId}, {Text}.",
            domainEvent.QuestionId,
            domainEvent.QuizId,
            domainEvent.Text);

        return Task.CompletedTask;
    }
}
