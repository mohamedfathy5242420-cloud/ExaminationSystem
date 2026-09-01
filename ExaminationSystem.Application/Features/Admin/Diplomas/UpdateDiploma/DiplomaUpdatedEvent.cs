using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;

public sealed record DiplomaUpdatedEvent(
    Guid DiplomaId,
    string Title,
    Guid InstructorId) : IEvent;
