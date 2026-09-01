using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;

public sealed record QuizSubmittedEvent(
    Guid AttemptId,
    Guid QuizId,
    Guid StudentId,
    int Score,
    bool IsPassed) : IEvent;
