using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Student.Quizzes.GetQuizTimer;

public sealed record QuizTimerExpiredEvent(
    Guid AttemptId,
    Guid QuizId,
    Guid StudentId,
    int Score,
    DateTime ExpiredAt) : IEvent;
