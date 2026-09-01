namespace ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas.ViewModels;

public sealed record InstructorDiplomaListItemViewModel(
    Guid Id,
    string Title,
    string Description,
    bool IsPublished,
    int QuizzesCount,
    int PublishedQuizzesCount,
    int EnrollmentsCount);
