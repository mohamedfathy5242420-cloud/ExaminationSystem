namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;

public sealed record StartQuizQuestionViewModel(
    Guid Id,
    string Text,
    int Score,
    IReadOnlyList<StartQuizOptionViewModel> Options);
