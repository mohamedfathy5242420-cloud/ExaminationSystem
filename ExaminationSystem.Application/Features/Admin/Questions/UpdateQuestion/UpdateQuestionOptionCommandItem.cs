namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;

public sealed record UpdateQuestionOptionCommandItem(
    string Text,
    bool IsCorrect);
