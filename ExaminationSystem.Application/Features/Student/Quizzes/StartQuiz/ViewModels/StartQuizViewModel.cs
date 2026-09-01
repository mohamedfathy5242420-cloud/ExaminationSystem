namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz.ViewModels;

public sealed record StartQuizViewModel(
    Guid AttemptId,
    Guid QuizId,
    string QuizTitle,
    DateTime StartedAt,
    DateTime EndsAt,
    int DurationInMinutes,
    int RemainingSeconds,
    IReadOnlyList<StartQuizQuestionViewModel> Questions);
