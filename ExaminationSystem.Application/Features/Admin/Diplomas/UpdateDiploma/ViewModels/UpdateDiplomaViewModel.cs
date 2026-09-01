namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.ViewModels;

public sealed record UpdateDiplomaViewModel(
    Guid Id,
    string Title,
    string Description,
    Guid InstructorId,
    bool IsPublished);
