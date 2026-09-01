namespace ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.ViewModels;

public sealed record UpdateQuizViewModel(
    Guid Id,
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions,
    bool IsPublished);
