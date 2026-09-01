using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class StudentEnrolledInDiplomaEventHandler : IEventHandler<StudentEnrolledInDiplomaEvent>
{
    private readonly ILogger<StudentEnrolledInDiplomaEventHandler> _logger;

    public StudentEnrolledInDiplomaEventHandler(
        ILogger<StudentEnrolledInDiplomaEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        StudentEnrolledInDiplomaEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Student enrolled: enrollment {EnrollmentId}, student {StudentId}, diploma {DiplomaId}.",
            domainEvent.EnrollmentId,
            domainEvent.StudentId,
            domainEvent.DiplomaId);

        return Task.CompletedTask;
    }
}
