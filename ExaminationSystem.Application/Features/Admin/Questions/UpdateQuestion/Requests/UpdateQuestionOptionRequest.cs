namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.Requests;

public sealed record UpdateQuestionOptionRequest(
    string Text,
    bool IsCorrect);
