namespace ExaminationSystem.Application.Features.Admin.Questions.GetQuizQuestions.ViewModels;

public sealed record QuestionOptionViewModel(
    Guid Id,
    string Text,
    bool IsCorrect);
