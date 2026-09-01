namespace ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz.ViewModels;

public sealed record SubmitQuizViewModel(
    Guid AttemptId,
    Guid QuizId,
    int Score,
    int TotalScore,
    int PassScore,
    bool IsPassed,
    string Status,
    DateTime SubmittedAt);
