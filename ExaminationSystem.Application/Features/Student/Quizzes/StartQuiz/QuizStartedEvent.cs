using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;

public sealed record QuizStartedEvent(
    Guid AttemptId,
    Guid QuizId,
    Guid StudentId,
    DateTime StartedAt) : IEvent;
