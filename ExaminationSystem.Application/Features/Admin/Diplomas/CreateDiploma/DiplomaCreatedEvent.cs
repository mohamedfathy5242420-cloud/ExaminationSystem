using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;

public sealed record DiplomaCreatedEvent(
    Guid DiplomaId,
    string Title,
    Guid InstructorId) : IEvent;
