namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.ViewModels;

public sealed record CreateDiplomaViewModel(
    Guid Id,
    string Title,
    string Description,
    Guid InstructorId,
    bool IsPublished);
