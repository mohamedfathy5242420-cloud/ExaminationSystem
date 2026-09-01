using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;

public sealed record QuizUnpublishedEvent(
    Guid QuizId,
    string Title) : IEvent;
