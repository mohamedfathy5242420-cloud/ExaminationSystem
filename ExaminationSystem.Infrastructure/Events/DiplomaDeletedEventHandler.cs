using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class DiplomaDeletedEventHandler : IEventHandler<DiplomaDeletedEvent>
{
    private readonly ILogger<DiplomaDeletedEventHandler> _logger;

    public DiplomaDeletedEventHandler(ILogger<DiplomaDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        DiplomaDeletedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Diploma deleted: {DiplomaId}, {Title}.",
            domainEvent.DiplomaId,
            domainEvent.Title);

        return Task.CompletedTask;
    }
}
