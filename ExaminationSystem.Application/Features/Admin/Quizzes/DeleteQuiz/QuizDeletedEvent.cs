using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;

public sealed record QuizDeletedEvent(
    Guid QuizId,
    string Title) : IEvent;
