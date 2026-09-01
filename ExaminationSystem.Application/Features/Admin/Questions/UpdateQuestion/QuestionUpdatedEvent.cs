using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;

public sealed record QuestionUpdatedEvent(
    Guid QuestionId,
    Guid QuizId,
    string Text) : IEvent;
