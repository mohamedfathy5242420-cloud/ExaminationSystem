namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.Requests;

public sealed record CreateQuestionOptionRequest(
    string Text,
    bool IsCorrect);
