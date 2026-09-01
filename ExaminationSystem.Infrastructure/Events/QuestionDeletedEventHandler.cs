using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class QuestionDeletedEventHandler : IEventHandler<QuestionDeletedEvent>
{
    private readonly ILogger<QuestionDeletedEventHandler> _logger;

    public QuestionDeletedEventHandler(ILogger<QuestionDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        QuestionDeletedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Question deleted: {QuestionId}, quiz {QuizId}, {Text}.",
            domainEvent.QuestionId,
            domainEvent.QuizId,
            domainEvent.Text);

        return Task.CompletedTask;
    }
}
