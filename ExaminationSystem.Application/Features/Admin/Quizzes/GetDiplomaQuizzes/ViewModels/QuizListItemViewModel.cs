namespace ExaminationSystem.Application.Features.Admin.Quizzes.GetDiplomaQuizzes.ViewModels;

public sealed record QuizListItemViewModel(
    Guid Id,
    Guid DiplomaId,
    string Title,
    int Duration,
    int PassScore,
    int MaxAttempts,
    string Instructions,
    bool IsPublished,
    DateTime CreatedOnUtc);
