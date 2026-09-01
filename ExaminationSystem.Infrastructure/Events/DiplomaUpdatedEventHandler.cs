using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class DiplomaUpdatedEventHandler : IEventHandler<DiplomaUpdatedEvent>
{
    private readonly ILogger<DiplomaUpdatedEventHandler> _logger;

    public DiplomaUpdatedEventHandler(ILogger<DiplomaUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        DiplomaUpdatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Diploma updated: {DiplomaId}, {Title}, instructor {InstructorId}.",
            domainEvent.DiplomaId,
            domainEvent.Title,
            domainEvent.InstructorId);

        return Task.CompletedTask;
    }
}
