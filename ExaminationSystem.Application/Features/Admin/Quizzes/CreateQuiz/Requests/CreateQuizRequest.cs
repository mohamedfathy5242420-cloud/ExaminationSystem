namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.Requests;

public sealed record CreateQuizRequest(
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions);
