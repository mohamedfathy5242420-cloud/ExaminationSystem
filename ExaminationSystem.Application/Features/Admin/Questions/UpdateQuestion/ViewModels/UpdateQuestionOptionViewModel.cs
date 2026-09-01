namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;

public sealed record UpdateQuestionOptionViewModel(
    Guid Id,
    string Text,
    bool IsCorrect);
