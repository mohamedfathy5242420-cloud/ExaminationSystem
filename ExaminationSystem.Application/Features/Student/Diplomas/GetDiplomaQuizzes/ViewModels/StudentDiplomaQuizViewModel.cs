namespace ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;

public sealed record StudentDiplomaQuizViewModel(
    Guid QuizId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions,
    int AttemptsUsed,
    int AttemptsRemaining,
    IReadOnlyList<StudentQuizAttemptSummaryViewModel> Attempts);
