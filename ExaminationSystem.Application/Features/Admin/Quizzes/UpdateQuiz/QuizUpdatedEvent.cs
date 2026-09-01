using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;

public sealed record QuizUpdatedEvent(
    Guid QuizId,
    Guid DiplomaId,
    string Title) : IEvent;
