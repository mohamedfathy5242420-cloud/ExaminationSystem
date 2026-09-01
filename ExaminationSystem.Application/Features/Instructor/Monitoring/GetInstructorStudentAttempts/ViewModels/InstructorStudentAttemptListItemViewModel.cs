namespace ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts.ViewModels;

public sealed record InstructorStudentAttemptListItemViewModel(
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
