namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;

public sealed record CreateQuestionOptionCommandItem(
    string Text,
    bool IsCorrect);
