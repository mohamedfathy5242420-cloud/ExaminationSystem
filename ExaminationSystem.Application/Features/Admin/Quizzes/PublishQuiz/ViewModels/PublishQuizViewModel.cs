namespace ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;

public sealed record PublishQuizViewModel(
    Guid Id,
    string Title,
    bool IsPublished,
    string Message);
