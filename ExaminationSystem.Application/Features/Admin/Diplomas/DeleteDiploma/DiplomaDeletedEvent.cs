using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;

public sealed record DiplomaDeletedEvent(
    Guid DiplomaId,
    string Title) : IEvent;
