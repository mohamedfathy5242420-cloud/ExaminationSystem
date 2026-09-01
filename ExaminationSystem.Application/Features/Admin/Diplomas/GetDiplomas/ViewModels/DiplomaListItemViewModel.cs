namespace ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas.ViewModels;

public sealed record DiplomaListItemViewModel(
    Guid Id,
    string Title,
    string Description,
    Guid InstructorId,
    bool IsPublished,
    DateTime CreatedOnUtc);
