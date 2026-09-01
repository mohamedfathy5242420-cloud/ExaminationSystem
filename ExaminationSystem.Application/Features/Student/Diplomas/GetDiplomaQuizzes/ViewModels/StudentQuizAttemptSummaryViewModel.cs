namespace ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;

public sealed record StudentQuizAttemptSummaryViewModel(
    Guid AttemptId,
    int Score,
    bool IsPassed,
    string Status,
    DateTime StartedAt,
    DateTime? EndedAt);
