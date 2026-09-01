namespace ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions.ViewModels;

public sealed record QuestionListItemViewModel(
    Guid Id,
    Guid QuizId,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<QuestionOptionViewModel> Options);
