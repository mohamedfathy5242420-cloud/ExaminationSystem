namespace ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.Requests;

public sealed record UpdateQuizRequest(
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions);
