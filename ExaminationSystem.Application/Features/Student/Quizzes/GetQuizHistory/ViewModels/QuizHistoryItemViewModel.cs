namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizHistory.ViewModels;

public sealed record QuizHistoryItemViewModel(
    Guid AttemptId,
    Guid QuizId,
    string QuizTitle,
    Guid DiplomaId,
    int Score,
    int PassScore,
    bool IsPassed,
    string Status,
    DateTime StartedAt,
    DateTime? EndedAt);
