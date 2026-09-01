namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.Requests;

public sealed record AnswerQuestionRequest(
    Guid AttemptId,
    Guid QuestionId,
    Guid SelectedOptionId);
