namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer.ViewModels;

public sealed record QuizTimerViewModel(
    Guid AttemptId,
    Guid QuizId,
    DateTime ServerNow,
    DateTime StartedAt,
    DateTime EndsAt,
    int RemainingSeconds,
    string Status,
    bool IsClosed);
