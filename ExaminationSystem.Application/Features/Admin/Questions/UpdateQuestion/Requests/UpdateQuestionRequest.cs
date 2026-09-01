namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.Requests;

public sealed record UpdateQuestionRequest(
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<UpdateQuestionOptionRequest> Options);
