namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.ViewModels;

public sealed record AnswerQuestionViewModel(
    Guid AttemptId,
    Guid QuestionId,
    Guid SelectedOptionId,
    DateTime AnsweredAt);
