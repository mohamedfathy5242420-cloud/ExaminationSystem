namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;

public sealed record CreateQuestionOptionViewModel(
    Guid Id,
    string Text,
    bool IsCorrect);
