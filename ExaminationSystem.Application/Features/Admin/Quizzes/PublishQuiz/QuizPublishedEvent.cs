using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;

public sealed record QuizPublishedEvent(
    Guid QuizId,
    string Title) : IEvent;
