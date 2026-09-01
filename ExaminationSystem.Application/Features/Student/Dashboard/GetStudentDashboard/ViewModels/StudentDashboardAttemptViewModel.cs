namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;

public sealed record StudentDashboardAttemptViewModel(
    Guid AttemptId,
    Guid QuizId,
    string QuizTitle,
    int Score,
    int PassScore,
    bool IsPassed,
    string Status,
    DateTime StartedAt,
    DateTime? EndedAt);
