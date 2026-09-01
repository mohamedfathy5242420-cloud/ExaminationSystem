namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;

public sealed record CreateQuestionViewModel(
    Guid Id,
    Guid QuizId,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<CreateQuestionOptionViewModel> Options);
