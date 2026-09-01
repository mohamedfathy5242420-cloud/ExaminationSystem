using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;

public sealed record QuestionAnsweredEvent(
    Guid AttemptId,
    Guid QuestionId,
    Guid SelectedOptionId,
    Guid StudentId) : IEvent;
