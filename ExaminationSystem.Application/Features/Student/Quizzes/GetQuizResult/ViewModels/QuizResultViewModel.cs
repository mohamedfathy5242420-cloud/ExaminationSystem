namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;

public sealed record QuizResultViewModel(
    Guid AttemptId,
    Guid QuizId,
    string QuizTitle,
    int Score,
    int TotalScore,
    int PassScore,
    bool IsPassed,
    string Status,
    DateTime StartedAt,
    DateTime? EndedAt,
    IReadOnlyList<QuizResultAnswerViewModel> Answers);
