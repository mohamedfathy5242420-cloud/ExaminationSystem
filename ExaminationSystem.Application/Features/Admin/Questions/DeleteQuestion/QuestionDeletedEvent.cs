using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;

public sealed record QuestionDeletedEvent(
    Guid QuestionId,
    Guid QuizId,
    string Text) : IEvent;
