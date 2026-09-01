using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;

public sealed record QuestionCreatedEvent(
    Guid QuestionId,
    Guid QuizId,
    string Text) : IEvent;
