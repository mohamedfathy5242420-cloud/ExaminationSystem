namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.ViewModels;

public sealed record CreateQuizViewModel(
    Guid Id,
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions,
    bool IsPublished);
