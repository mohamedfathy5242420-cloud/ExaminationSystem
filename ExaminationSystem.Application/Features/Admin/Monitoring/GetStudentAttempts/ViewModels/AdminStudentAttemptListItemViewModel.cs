namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts.ViewModels;

public sealed record AdminStudentAttemptListItemViewModel(
    Guid AttemptId,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    Guid QuizId,
    string QuizTitle,
    Guid DiplomaId,
    string DiplomaTitle,
    string Status,
    int Score,
    int PassScore,
    bool IsPassed,
    DateTime StartedAt,
    DateTime? EndedAt);
