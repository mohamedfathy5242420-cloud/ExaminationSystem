namespace ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas.ViewModels;

public sealed record StudentDiplomaListItemViewModel(
    Guid Id,
    string Title,
    string Description,
    Guid InstructorId,
    bool IsEnrolled,
    decimal Progress,
    int PublishedQuizzesCount,
    int CompletedAttemptsCount);
