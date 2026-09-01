using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;

public sealed record QuizCreatedEvent(
    Guid QuizId,
    Guid DiplomaId,
    string Title) : IEvent;
