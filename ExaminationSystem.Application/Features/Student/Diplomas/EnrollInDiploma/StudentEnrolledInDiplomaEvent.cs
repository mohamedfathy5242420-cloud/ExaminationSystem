using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;

public sealed record StudentEnrolledInDiplomaEvent(
    Guid EnrollmentId,
    Guid StudentId,
    Guid DiplomaId,
    DateTime EnrolledAt) : IEvent;
