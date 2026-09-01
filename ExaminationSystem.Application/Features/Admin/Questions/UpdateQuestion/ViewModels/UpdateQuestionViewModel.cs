namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;

public sealed record UpdateQuestionViewModel(
    Guid Id,
    Guid QuizId,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<UpdateQuestionOptionViewModel> Options);
