namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

public sealed record FailedQuestionViewModel(
    Guid QuestionId,
    string QuestionText,
    Guid QuizId,
    string QuizTitle,
    int FailedAnswersCount);
