namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.Requests;

public sealed record CreateQuestionRequest(
    Guid QuizId,
    string Text,
    string Explanation,
    int Order,
    int Score,
    IReadOnlyList<CreateQuestionOptionRequest> Options);
