using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class DiplomaCreatedEventHandler : IEventHandler<DiplomaCreatedEvent>
{
    private readonly ILogger<DiplomaCreatedEventHandler> _logger;

    public DiplomaCreatedEventHandler(ILogger<DiplomaCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        DiplomaCreatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Diploma created: {DiplomaId}, {Title}, instructor {InstructorId}.",
            domainEvent.DiplomaId,
            domainEvent.Title,
            domainEvent.InstructorId);

        return Task.CompletedTask;
    }
}
